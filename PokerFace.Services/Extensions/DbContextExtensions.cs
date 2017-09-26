namespace PokerFace.Services.Extensions
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;

    public static class DbContextExtensions
    {
        public static bool HasMigrations(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return total.Except(applied).Any();
        }

        public static bool HasNoMigrations(this DbContext context)
        {
            return context.HasMigrations() == false;
        }
    }
}