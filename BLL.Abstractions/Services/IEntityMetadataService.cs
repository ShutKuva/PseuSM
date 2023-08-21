using BLL.Entities;
using System.Linq.Expressions;

namespace BLL.Abstractions.Services
{
    public interface IEntityMetadataService<TEntity, TDalEntity, TParams> : IStreamMetadataService<TEntity, TParams>
    {
        Task AttachEntityToAllMetadataFromSourceImageAsync(TEntity sourceImage, TEntity image);
        Task<string> GetReferenceAsync(TEntity image);
        Task DeleteMetadataAsync(TEntity metadata);
    }
}
