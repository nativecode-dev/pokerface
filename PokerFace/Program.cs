﻿namespace PokerFace
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Services.Data;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Program.MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<ProgramStartup>()
                .Build();

            await PokerFaceDataContext.InitializeAsync(host.Services).ConfigureAwait(true);

            await host.RunAsync().ConfigureAwait(false);
        }
    }
}