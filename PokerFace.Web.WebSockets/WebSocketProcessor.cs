namespace PokerFace.Web.WebSockets
{
    using System;
    using System.Collections.Concurrent;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Extensions;
    using Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class WebSocketProcessor : IWebSocketProcessor
    {
        private static readonly ConcurrentDictionary<Guid, WebSocket> Sockets =
            new ConcurrentDictionary<Guid, WebSocket>();

        private readonly IMapper mapper;

        private readonly IMediator mediator;

        public WebSocketProcessor(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public Task StartAsync(HttpContext context, CancellationToken token = default(CancellationToken))
        {
            if (context.WebSockets.IsWebSocketRequest == false)
            {
                return Task.CompletedTask;
            }

            var path = context.Request.Path;

            if (path.HasValue && path.Value.ToLower() == "/ws")
            {
                return this.RunAsync(context.WebSockets.AcceptWebSocketAsync(), token);
            }

            return Task.CompletedTask;
        }

        private async Task RunAsync(Task<WebSocket> task, CancellationToken token)
        {
            using (var socket = await task.NoCapture())
            {
                if (WebSocketProcessor.Sockets.TryAdd(Guid.NewGuid(), socket))
                {
                    while (token.IsCancellationRequested == false && socket.State == WebSocketState.Open)
                    {
                        await this.MessageLoopAsync(token, socket).NoCapture();
                    }
                }
            }
        }

        private async Task MessageLoopAsync(CancellationToken token, WebSocket socket)
        {
            var message = await socket.GetNextTextAsync(token).NoCapture();

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var jobject = JObject.Parse(message);
            var request = new WebSocketRequest<JObject> {Data = jobject};

            var json = await this.mediator
                .Send<JObject>(request, token)
                .NoCapture();

            var response = this.mapper.Map<WebSocketResponse>(json);

            if (response == null || response.Type == WebSocketResponseType.Ignore)
            {
                return;
            }

            var text = JsonConvert.SerializeObject(response);

            if (response.Type == WebSocketResponseType.Broadcast)
            {
                await Task.WhenAll(WebSocketProcessor.Sockets.Values.BroadcastText(text, token)).NoCapture();
            }
            else if (response.Type == WebSocketResponseType.Reply)
            {
                await socket.SendTextAsync(text, token).NoCapture();
            }
        }
    }
}