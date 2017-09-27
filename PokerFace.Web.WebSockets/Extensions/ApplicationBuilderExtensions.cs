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

            return app.UseWebSockets(options)
                .Use(ApplicationBuilderExtensions.WebSocketHandler);
        }

        private static async Task WebSocketHandler(HttpContext context, Func<Task> next)
        {
            await next().NoCapture();

            try
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var processor = context.RequestServices.GetService<IWebSocketProcessor>();

                    using (var socket = await context.WebSockets.AcceptWebSocketAsync().NoCapture())
                    {
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