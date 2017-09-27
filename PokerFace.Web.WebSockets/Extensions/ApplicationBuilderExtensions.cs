namespace PokerFace.Web.WebSockets.Extensions
{
    using System;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePokerFaceWebSockets(this IApplicationBuilder app)
        {
            var options = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 8192
            };

            return app.UseWebSockets(options).Use(ApplicationBuilderExtensions.WebSocketHandler);
        }

        public static void PokerFaceWebSockets(IApplicationBuilder app)
        {
            app.UsePokerFaceWebSockets();
        }

        private static async Task WebSocketHandler(HttpContext context, Func<Task> next)
        {
            // NOTE: We have to run all of the other middleware before we try to upgrade
            // the connection. There didn't seem to be a good spot to ensure this is called
            // only before closing the websocket, so we do it here first. Otherwise, we end
            // up with an ugly branching situation. Something might come along that changes
            // the validity of this assumption.
            await next().NoCapture();

            try
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var processor = context.RequestServices.GetService<IWebSocketProcessor>();

                    using (var socket = await context.WebSockets.AcceptWebSocketAsync().NoCapture())
                    {
                        // NOTE: It's import that the processor NOT call CloseAsync on the websocket
                        // because other middleware might try to set the status code, which is why
                        // all the other middleware is run first.
                        await processor.StartAsync(socket, context.RequestAborted).NoCapture();
                    }
                }
            }
            catch (Exception ex)
            {
                var factory = context.RequestServices.GetService<ILoggerFactory>();
                var logger = factory.CreateLogger<WebSocket>();
                logger.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}