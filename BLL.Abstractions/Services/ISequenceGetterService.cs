namespace BLL.Abstractions.Services
{
    public interface ISequenceGetterService<TEntity, TParams>
    {
        Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(TParams parameters);
    }
}
