using DAL.Abstractions.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories.BaseRepositories
{
    public class BaseRepository<TEntity, TId, TPredicate> : IRepository<TEntity, TId, TPredicate>
        where TEntity : class
    {
        protected readonly MainContext _context;

        public BaseRepository(MainContext context)
        {
            _context = context;
        }

        public virtual async Task CreateEntityAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await _context.Set<TEntity>().AddAsync(entity);
        }

        public Task DeleteEntityAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(TPredicate predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityByIdAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
