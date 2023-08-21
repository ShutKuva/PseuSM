using DAL.Entities;

namespace BLL.Abstractions.Services.DalServices
{
    public interface IDalEntityService<TDalEntity>
    {
        public Task<TDalEntity?> GetDalEntityByIdAsync(int id);
    }
}
