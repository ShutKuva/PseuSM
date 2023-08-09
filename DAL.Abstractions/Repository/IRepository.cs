namespace DAL.Abstractions.Repository
{
    public interface IRepository<TEntity, TId, TPredicate>
    {
        Task<TEntity> GetEntityByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(TPredicate predicate);
        Task<bool> CreateEntityAsync(TEntity entity);
        Task<bool> UpdateEntityAsync(TEntity entity);
        Task<bool> DeleteEntityAsync(TId id);
    }
}
