namespace PokerFace
{
    using System;
    using AutoMapper;
    using Core.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services.Extensions;
    using Web.WebSockets.Extensions;

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
                .AddAutoMapper()
                .AddTransient<IRandomNameService, RandomNameService>()
                .AddPokerFaceServices(this.configuration)
                .AddPokerFaceWebSockets()
                .AddMvc()
                .Services
                .BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseMvcWithDefaultRoute();
            app.UsePokerFaceWebSockets();
            app.UseStaticFiles();
        }
    }
}