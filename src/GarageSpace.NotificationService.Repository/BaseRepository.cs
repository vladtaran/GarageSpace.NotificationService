using GarageSpace.NotificationService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GarageSpace.NotificationService.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        public DbSet<TEntity>? dbSet;
        public readonly DbContext dbContext;
        public DbSet<TEntity> DbSet => dbSet ??= dbContext.Set<TEntity>();

        protected BaseRepository(
            DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected virtual IQueryable<TEntity> InitQuery() => DbSet;

        protected virtual async IAsyncEnumerable<TEntity> GetAll()
        {
            var entities = await DbSet.ToListAsync();
            foreach (var entity in entities)
            {
                yield return entity;
            }
        }
    }
}
