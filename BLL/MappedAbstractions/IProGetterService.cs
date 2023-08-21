using BLL.Entities.BaseEntities;
using System.Linq.Expressions;
using DALBaseEntity = DAL.Entities.BaseEntities.BaseEntity;

namespace BLL.MappedAbstractions
{
    public interface IProGetterService<TEntity, TDalEntity>
        where TEntity : BaseEntity
        where TDalEntity : DALBaseEntity
    {
        Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TDalEntity, bool>> predicate);
    }
}
