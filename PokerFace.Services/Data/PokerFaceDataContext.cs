namespace PokerFace.Services.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Poker;

    public partial class PokerFaceDataContext : DbContext
    {
        public PokerFaceDataContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.UpdateAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            this.UpdateAuditProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateAuditProperties()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                var model = entry.Entity as Entity;

                if (model == null)
                {
                    continue;
                }

                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity<Guid> keymodel && keymodel.Id == Guid.Empty)
                    {
                        keymodel.Id = Guid.NewGuid();
                    }

                    model.DateCreated = DateTimeOffset.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    model.DateModified = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}