namespace PokerFace.Services.Extensions
{
    using Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokerFaceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(PokerFaceDataContext));

            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            return services
                .AddDbContext<PokerFaceDataContext>(options => options.UseMySql(connectionString))
                .AddEntityFrameworkMySql()
                .AddTransient<IGameService, GameService>();
        }
    }
}