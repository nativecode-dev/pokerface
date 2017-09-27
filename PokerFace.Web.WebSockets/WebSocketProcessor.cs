namespace PokerFace.Web.WebSockets
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Extensions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class WebSocketProcessor : IWebSocketProcessor
    {
        private static readonly ConcurrentDictionary<Guid, WebSocket> Sockets =
            new ConcurrentDictionary<Guid, WebSocket>();

        private readonly ILogger logger;

        private readonly IMediator mediator;

        public WebSocketProcessor(ILoggerFactory factory, IMediator mediator)
        {
            this.logger = factory.CreateLogger<WebSocketProcessor>();
            this.mediator = mediator;
        }

        public async Task StartAsync(WebSocket socket, CancellationToken token = default(CancellationToken))
        {
            var key = Guid.NewGuid();

            if (WebSocketProcessor.Sockets.TryAdd(key, socket))
            {
                while (token.IsCancellationRequested == false && socket.State == WebSocketState.Open)
                {
                    try
                    {
                        await this.MessageLoopAsync(socket, token).NoCapture();
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex.Message, ex.StackTrace);
                    }
                }

                if (WebSocketProcessor.Sockets.TryRemove(key, out var _) == false)
                {
                    throw new InvalidOperationException("Failed to remove socket.");
                }
            }
        }

        private async Task MessageLoopAsync(WebSocket socket, CancellationToken token)
        {
            var request = await socket.GetNextAsync<WebSocketRequest<JObject>>(token).NoCapture();

            if (request == null)
            {
                return;
            }

            var json = await this.mediator
                .Send<JObject>(request, token)
                .NoCapture();

            // TODO: AutoMapper doesn't seem to map the enum correctly.
            var response = JsonConvert.DeserializeObject<WebSocketResponse>(json.ToString());

            if (response == null || response.Type == WebSocketResponseType.Ignore)
            {
                return;
            }

            if (response.Type == WebSocketResponseType.Broadcast)
            {
                var sockets = WebSocketProcessor.Sockets.Values
                    .Where(v => v != socket && v.State == WebSocketState.Open);

                await Task.WhenAll(sockets.Broadcast(response, token)).NoCapture();
            }
            else
            {
                await socket.SendAsync(response, token).NoCapture();
            }
        }
    }
}