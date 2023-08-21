using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;

namespace BLL.Services.BaseServices
{
    public class OnlyGetRepositoryServiceBase<TDalEntity>
        where TDalEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>>? _entityRepository;

        public OnlyGetRepositoryServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>>> GetRepositoryAsync()
        {
            _entityRepository ??= await _unitOfWork.GetRepositoryAsync<TDalEntity, int, Expression<Func<TDalEntity, bool>>>()
                    ?? throw new InvalidOperationException($"Cannot resolve {typeof(TDalEntity).Name}Repository");

            return _entityRepository;
        }
    }
}
