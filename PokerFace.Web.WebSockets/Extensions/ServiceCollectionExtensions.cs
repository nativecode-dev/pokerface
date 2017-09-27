namespace PokerFace.Web.WebSockets.Extensions
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokerFaceWebSockets(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
            services.AddTransient<IWebSocketProcessor, WebSocketProcessor>();

            return services;
        }
    }
}