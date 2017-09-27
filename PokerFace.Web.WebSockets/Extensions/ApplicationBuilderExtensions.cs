namespace PokerFace.Web.WebSockets.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePokerFaceWebSockets(this IApplicationBuilder app)
        {
            return app.UseWebSockets().Use(ApplicationBuilderExtensions.WebSocketHandler);
        }

        private static async Task WebSocketHandler(HttpContext context, Func<Task> next)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using (var cts = new CancellationTokenSource())
                {
                    try
                    {
                        var processor = context.RequestServices.GetService<IWebSocketProcessor>();
                        await processor.StartAsync(context, cts.Token).NoCapture();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }

            await next().NoCapture();
        }
    }
}