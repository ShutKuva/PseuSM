using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DALImage = DAL.Entities.Image;

namespace BLL.Services
{
    public class ImageService : ServiceBase<DALImage>, IImageService
    {
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task CreateImage(Image image, User user)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            DALImage newImage = _mapper.Map<DALImage>(image);
            newImage.UserId = user.Id;

            await imageRepository.CreateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteImage(Image image)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            await imageRepository.DeleteEntityAsync(image.Id);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateImage(Image image)
        {
            IRepository<DALImage, int, System.Linq.Expressions.Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            DALImage? oldImage = await imageRepository.GetEntityByIdAsync(image.Id);

            DALImage newImage = _mapper.Map<DALImage>(image);
            newImage.UserId = oldImage?.UserId ?? newImage.UserId;

            await imageRepository.UpdateEntityAsync(newImage);

            await _unitOfWork.CommitAsync();
        }
    }
}
