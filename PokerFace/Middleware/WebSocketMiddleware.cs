namespace PokerFace.Middleware
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net.Http;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.AspNetCore.Http;

    public class WebSocketMiddleware
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> sockets = new ConcurrentDictionary<Guid, WebSocket>();

        private readonly RequestDelegate next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest == false)
            {
                await this.next(context).ConfigureAwait(false);
                return;
            }

            var id = Guid.NewGuid();
            var token = context.RequestAborted;
            var socket = await context.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);

            if (this.sockets.TryAdd(id, socket) == false)
            {
                throw new HttpRequestException($"Failed to add websocket {id} to tracker.");
            }

            while (token.IsCancellationRequested == false && socket.State == WebSocketState.Open)
            {
                var response = await socket.GetNextTextAsync(token).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(response))
                {
                    continue;
                }

                var tasks = this.sockets.Values.Select(s => s.BroadcastTextAsync(response, token));
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }

            if (this.sockets.TryRemove(id, out var disposable))
            {
                disposable.Dispose();
            }
        }
    }
}