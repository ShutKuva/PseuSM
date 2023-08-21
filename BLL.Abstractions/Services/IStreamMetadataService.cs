namespace BLL.Abstractions.Services
{
    public interface IStreamMetadataService<TEntity, TParams>
    {
        Task CreateMetadataFromStreamAsync(Stream stream, TEntity image, TParams parameters);
    }
}
