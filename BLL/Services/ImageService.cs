using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.MappedAbstractions;
using BLL.Parameters;
using BLL.Services.BaseServices;
using Core.Enums;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALImage = DAL.Entities.Image;

namespace BLL.Services
{
    public class ImageService : ServiceBase<Image, DALImage>, IImageService
    {
        private readonly IEntityMetadataService<Image, DALImage, CloudinaryParameters> _imageMetadataService;

        public ImageService(
            IUnitOfWork unitOfWork,
            ICloudinaryMetadataService<Image, DALImage> imageMetadataService,
            IMapper mapper) : base(unitOfWork, mapper)
        {
            _imageMetadataService = imageMetadataService;
        }

        public async Task<Image?> GetImageByIdAsync(int id)
        {
            IRepository<DALImage, int, Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            return _mapper.Map<Image?>(await imageRepository.GetEntityByIdAsync(id));
        }

        public Task<Image> CreateImageAsync(Image image, User? user)
        {
            DALImage newImage = _mapper.Map<DALImage>(image);
            newImage.Id = default;

            if (user != null)
            {
                newImage.UserId = user.Id;
            }

            return CreateEntityAsync(newImage);
        }

        public async Task<Image> CreateImageFromStreamAsync(Stream stream, ImageTypes imageType, User? user)
        {
            var newImage = new DALImage()
            {
                Type = imageType,
            };

            if (user != null)
            {
                newImage.UserId = user.Id;
            }

            Image mappedCopyOfDalImage = await CreateEntityAsync(newImage);

            await _imageMetadataService.CreateMetadataFromStreamAsync(stream, mappedCopyOfDalImage, new CloudinaryParameters { User = user });

            mappedCopyOfDalImage.Reference = newImage.Reference = await _imageMetadataService.GetReferenceAsync(mappedCopyOfDalImage);

            await UpdateDalEntityAsync(newImage);

            return mappedCopyOfDalImage;
        }

        public Task DeleteImageAsync(Image image)
        {
            return DeleteEntityAsync(image.Id);
        }

        public async Task UpdateImageAsync(Image image)
        {
            IRepository<DALImage, int, Expression<Func<DALImage, bool>>> imageRepository = await GetRepositoryAsync();

            DALImage? oldImage = await imageRepository.GetEntityByIdAsync(image.Id);

            DALImage newImage = _mapper.Map<DALImage>(image);
            newImage.UserId = oldImage?.UserId ?? newImage.UserId;

            await UpdateDalEntityAsync(newImage);
        }
    }
}
