using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using Core.Enums;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALImagePlaceholder = DAL.Entities.ImagePlaceholder;

namespace BLL.Services
{
    public class ImagePlaceholderService : ServiceBase<ImagePlaceholder, DALImagePlaceholder>, IImagePlaceholderService
    {
        private readonly IImageService _imageService;

        public ImagePlaceholderService(
            IImageService imageService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
            _imageService = imageService;
        }

        public async Task<ImagePlaceholder> CreateImagePlaceholderFromStreamAsync(Stream stream, ImageTypes placeholderType)
        {
            Image placeholderImage = await _imageService.CreateImageFromStreamAsync(stream, placeholderType, null);

            var dalImagePlaceholder = new DALImagePlaceholder
            {
                ImageId = placeholderImage.Id,
                Type = placeholderType,
            };

            return await CreateEntityAsync(dalImagePlaceholder);
        }

        public async Task<Image> CreateImageFromImagePlaceholderAsync(ImageTypes placeholderType, User user)
        {
            ImagePlaceholder imagePlaceholder = await GetImagePlaceholderByImageType(placeholderType);

            return await _imageService.CreateImageAsync(imagePlaceholder.Image, user);
        }

        public Task DeleteImagePlaceholderAsync(ImagePlaceholder placeholder)
        {
            return DeleteEntityAsync(placeholder.Id);
        }

        public async Task<ImagePlaceholder> GetImagePlaceholderByImageType(ImageTypes placeholderType)
        {
            return (await GetImagePlaceholdersByImageType(placeholderType)).First();
        }

        public async Task<IEnumerable<ImagePlaceholder>> GetImagePlaceholdersByImageType(ImageTypes placeholderType)
        {
            IRepository<DALImagePlaceholder, int, Expression<Func<DALImagePlaceholder, bool>>> imagePlaceholderRepository = await GetRepositoryAsync();

            IEnumerable<DALImagePlaceholder> imagePlaceholderWithSameType = await imagePlaceholderRepository.GetEntitiesByPredicateAsync(imagePlaceholder => imagePlaceholder.Type == placeholderType);

            if (!imagePlaceholderWithSameType.Any())
            {
                throw new ArgumentException("There is no image placeholder of specified type");
            }

            return _mapper.Map<IEnumerable<ImagePlaceholder>>(imagePlaceholderWithSameType);
        }
    }
}
