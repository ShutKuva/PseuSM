using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using DALCloudinaryImage = DAL.Entities.Cloudinary.CloudinaryImage;

namespace BLL.Services
{
    public class CloudinaryImageService : ServiceBase<DALCloudinaryImage>, IImageMetadataService<User>
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageService(IUnitOfWork unitOfWork, IOptions<CloudinarySettings> cloudinarySettingsOptions) : base(unitOfWork)
        {
            CloudinarySettings cloudinarySettings = cloudinarySettingsOptions.Value ?? throw new ArgumentNullException(nameof(cloudinarySettingsOptions));
            _cloudinary = new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
        }

        public async Task CreateMetadataFromStream(Stream stream, Image image, User user)
        {
            DateTime now = DateTime.Now;

            ImageUploadResult imageUploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
            {
                File = new FileDescription($"{user.Id}_{user.Login}_{now.Year}_{now.Day}_{now.Minute}", stream)
            });

            if (imageUploadResult.Error == null)
            {
                throw new InvalidOperationException($"Unable to upload image: {imageUploadResult.Error}");
            }

            var cloudinaryImage = new DALCloudinaryImage
            {
                PublicId = imageUploadResult.PublicId,
                ImageId = image.Id
            };

            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            await cloudinaryImageRepository.CreateEntityAsync(cloudinaryImage);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMetadata(Image image)
        {
            IRepository<DALCloudinaryImage, int, Expression<Func<DALCloudinaryImage, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            IEnumerable<DALCloudinaryImage> delCloudinaryImages = await cloudinaryImageRepository.GetEntitiesByPredicateAsync(cloudinaryImage => cloudinaryImage.ImageId == image.Id);

            foreach (DALCloudinaryImage cloudinaryImage in delCloudinaryImages)
            {
                DelResResult delResult = await _cloudinary.DeleteResourcesAsync(ResourceType.Image, cloudinaryImage.PublicId);

                if (delResult.Error is null)
                {
                    throw new InvalidOperationException($"Unable to delete image: {delResult.Error}");
                }

                await cloudinaryImageRepository.DeleteEntityAsync(cloudinaryImage.Id);
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
