namespace PokerFace
{
    using System;
    using AutoMapper;
    using Core.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Services.Extensions;
    using Web.WebSockets.Extensions;
    using ApplicationBuilderExtensions = Web.WebSockets.Extensions.ApplicationBuilderExtensions;

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
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                })
                .Services
                .BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.Map("/ws", ApplicationBuilderExtensions.PokerFaceWebSockets);
        }
    }
}