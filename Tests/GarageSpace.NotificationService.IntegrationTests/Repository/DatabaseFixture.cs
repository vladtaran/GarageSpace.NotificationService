using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GarageSpace.NotificationService.Repository.Infrastructure;
using Xunit;

namespace GarageSpace.IntegrationTests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            await using var db = CreateDbContext();
            await db.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await CleanupDatabase();
        }
        public MainDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            return new MainDbContext(options);
        }

        public async Task CleanupDatabase()
        {
            await using var db = CreateDbContext();

            db.Users.RemoveRange(db.Users);
            await db.SaveChangesAsync();
        }
    }
}
