namespace PokerFace.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class PokerFaceDataContextFactory : IDesignTimeDbContextFactory<PokerFaceDataContext>
    {
        public PokerFaceDataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PokerFaceDataContext>()
                .UseMySql("Server=localhost; Database=pokerface; User Id=pokerface; Password=pokerface");

            return new PokerFaceDataContext(builder.Options);
        }
    }
}