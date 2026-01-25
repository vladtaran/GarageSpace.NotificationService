using Microsoft.EntityFrameworkCore;
using GarageSpace.NotificationService.Models.Domain;

namespace GarageSpace.NotificationService.Repository.Infrastructure
{
    public class MainDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}