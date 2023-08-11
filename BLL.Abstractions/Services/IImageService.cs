using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IImageService
    {
        Task CreateImage(Image image, User user);
        Task DeleteImage(Image image);
        Task UpdateImage(Image image);
    }
}
