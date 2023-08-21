using BLL.Entities;
using Core.Enums;

namespace BLL.Abstractions.Services
{
    public interface IImagePlaceholderService
    {
        Task<ImagePlaceholder> CreateImagePlaceholderFromStreamAsync(Stream stream, ImageTypes placeholderType);
        Task<Image> CreateImageFromImagePlaceholderAsync(ImageTypes placeholderType, User user);
        Task<IEnumerable<ImagePlaceholder>> GetImagePlaceholdersByImageType(ImageTypes placeholderType);
        Task<ImagePlaceholder> GetImagePlaceholderByImageType(ImageTypes placeholderType);
        Task DeleteImagePlaceholderAsync(ImagePlaceholder placeholder);
    }
}
