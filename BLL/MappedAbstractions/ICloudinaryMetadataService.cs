using BLL.Abstractions.Services;
using BLL.Parameters;

namespace BLL.MappedAbstractions
{
    public interface ICloudinaryMetadataService<TEntity, TDalEntity> : IEntityMetadataService<TEntity, TDalEntity, CloudinaryParameters>
    {
    }
}
