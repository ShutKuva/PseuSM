using AutoMapper;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities.BaseEntities;
using System.Linq.Expressions;

namespace BLL.Services.BaseServices
{
    public class ServiceBase<TEntity, TDalEntity> : OnlyGetRepositoryServiceBase<TDalEntity>
        where TEntity : class
        where TDalEntity : BaseEntity
    {
        protected readonly IMapper _mapper;

        public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        protected async Task<TEntity> CreateEntityAsync(TDalEntity dalEntity)
        {
            IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>> entityRepository = await GetRepositoryAsync();

            await entityRepository.CreateEntityAsync(dalEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TEntity>(dalEntity);
        }

        protected async Task UpdateDalEntityAsync(TDalEntity dalEntity)
        {
            IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>> entityRepository = await GetRepositoryAsync();

            await entityRepository.UpdateEntityAsync(dalEntity);
            await _unitOfWork.CommitAsync();
        }

        protected async Task DeleteEntityAsync(int? id = null, Expression<Func<TDalEntity, bool>>? predicate = null, string? errorMessage = null)
        {
            IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>> entityRepository = await GetRepositoryAsync();

            if (predicate is not null)
            {
                IEnumerable<TDalEntity> entitiesThatMeetCondition = await entityRepository.GetEntitiesByPredicateAsync(predicate);

                if (!entitiesThatMeetCondition.Any() && errorMessage is not null)
                {
                    throw new ArgumentException(errorMessage);
                }

                await entityRepository.DeleteEntityAsync(entitiesThatMeetCondition.First().Id);
            } 
            else
            {
                if (id is null)
                {
                    throw new ArgumentException("Id or predicate must be specified");
                }

                await entityRepository.DeleteEntityAsync(id.Value);
            }
            
            await _unitOfWork.CommitAsync();
        }
    }
}
