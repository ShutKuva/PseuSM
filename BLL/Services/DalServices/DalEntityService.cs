using AutoMapper;
using BLL.Abstractions.Services.DalServices;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities.BaseEntities;
using System.Linq.Expressions;

namespace BLL.Services.DalServices
{
    public class DalEntityService<TDalEntity> : OnlyGetRepositoryServiceBase<TDalEntity>, IDalEntityService<TDalEntity>
        where TDalEntity : BaseEntity
    {
        public DalEntityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TDalEntity?> GetDalEntityByIdAsync(int id)
        {
            IRepository<TDalEntity, int, Expression<Func<TDalEntity, bool>>> entityRepository = await GetRepositoryAsync();

            return await entityRepository.GetEntityByIdAsync(id);
        }
    }
}
