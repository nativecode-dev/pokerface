namespace PokerFace
{
    using System;
    using Core.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Services.Extensions;

    public class ProgramStartup
    {
        private readonly IConfiguration configuration;

        public ProgramStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services
                .AddTransient<IRandomNameService, RandomNameService>()
                .AddPokerFaceData(this.configuration)
                .AddMvc()
                .Services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<WebSocketMiddleware>();
            app.UseMvcWithDefaultRoute();
            app.UseWebSockets();
        }
    }
}