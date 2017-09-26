namespace PokerFace.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public partial class PokerFaceDataContext
    {
        public static async Task InitializeAsync(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            using (var context = scope.ServiceProvider.GetService<PokerFaceDataContext>())
            {
                if (context.HasMigrations())
                {
                    await context.Database.MigrateAsync().ConfigureAwait(true);
                }
            }
        }
    }
}