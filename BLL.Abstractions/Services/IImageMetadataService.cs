using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IImageMetadataService<TParams>
    {
        public Task CreateMetadataFromStream(Stream stream, Image image, TParams parameters);
        public Task DeleteMetadata(Image metadata);
    }
}
