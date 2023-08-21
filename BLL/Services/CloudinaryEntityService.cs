using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Abstractions.Services.DalServices;
using BLL.Abstractions.Services.Externals;
using BLL.Abstractions.Tools;
using BLL.Entities;
using BLL.Entities.BaseEntities;
using BLL.MappedAbstractions;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using DALBaseEntity = DAL.Entities.BaseEntities.BaseEntity;
using DALCloudinaryEntity = DAL.Entities.Cloudinary.CloudinaryEntity;

namespace BLL.Services
{
    public class CloudinaryEntityService<TEntity, TDalEntity> : ServiceBase<CloudinaryEntity, DALCloudinaryEntity>, ICloudinaryMetadataService<TEntity, TDalEntity>
        where TEntity : BaseEntity
        where TDalEntity : DALBaseEntity
    {
        private readonly Func<TDalEntity, DALCloudinaryEntity?> GetCloudinaryEntity;
        private readonly Func<DALCloudinaryEntity, ICollection<TDalEntity>> GetCollection;

        private readonly Core.CloudinaryParameters _cloudinaryParameters;
        private readonly IDalEntityService<TDalEntity> _entityService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IParamsResolver<string, Parameters.CloudinaryParameters> _nameResolver;

        public CloudinaryEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDalEntityService<TDalEntity> entityService,
            ICloudinaryService cloudinaryService, 
            IParamsResolver<string, Parameters.CloudinaryParameters> nameResolver,
            IOptions<Core.CloudinaryParameters> cloudinaryParametersOptions) : base(unitOfWork, mapper)
        {
            _cloudinaryParameters = cloudinaryParametersOptions.Value ?? throw new ArgumentNullException(nameof(cloudinaryParametersOptions));
            _entityService = entityService;
            _cloudinaryService = cloudinaryService;
            _nameResolver = nameResolver;

            ParameterExpression dalEntityParameter = Expression.Parameter(typeof(TDalEntity), "dalEntity");
            Expression cloudinaryEntityProperty = Expression.Property(dalEntityParameter, nameof(CloudinaryEntity));
            GetCloudinaryEntity = Expression.Lambda<Func<TDalEntity, DALCloudinaryEntity>>(cloudinaryEntityProperty, dalEntityParameter).Compile();

            ParameterExpression dalCloudinaryEntityParameter = Expression.Parameter(typeof(DALCloudinaryEntity), "dalCloudinaryEntity");
            Expression collectionProperty = Expression.Property(dalCloudinaryEntityParameter, typeof(TDalEntity).Name + "s");
            GetCollection = Expression.Lambda<Func<DALCloudinaryEntity, ICollection<TDalEntity>>>(collectionProperty, dalCloudinaryEntityParameter).Compile();
        }

        public async Task AttachEntityToAllMetadataFromSourceImageAsync(TEntity sourceEntity, TEntity entity)
        {
            TDalEntity dalEntity = await GetDalEntityAsync(entity.Id);
            TDalEntity dalSourceEntity = await GetDalEntityAsync(sourceEntity.Id);

            DALCloudinaryEntity extractedEntity = GetCloudinaryEntity(dalSourceEntity) ?? throw new ArgumentException($"There is no metadata attached to entity {entity.Id}");

            GetCollection(extractedEntity).Add(dalEntity);

            await _unitOfWork.CommitAsync();
        }

        public async Task CreateMetadataFromStreamAsync(Stream stream, TEntity image, Parameters.CloudinaryParameters parameters)
        {
            TDalEntity dalEntity = await GetDalEntityAsync(image.Id);

            string publicId = await _cloudinaryService.UploadImageAsync(stream, await _nameResolver.CreateParamsAsync(parameters));

            var cloudinaryEntity = new DALCloudinaryEntity
            {
                PublicId = publicId,
            };

            GetCollection(cloudinaryEntity).Add(dalEntity);

            await CreateEntityAsync(cloudinaryEntity);
        }

        public async Task DeleteMetadataAsync(TEntity entity)
        {
            TDalEntity dalEntity = await GetDalEntityAsync(entity.Id);

            DALCloudinaryEntity extractedEntity = GetCloudinaryEntity(dalEntity) ?? throw new ArgumentException($"There is no metadata attached to entity {entity.Id}");

            IRepository<DALCloudinaryEntity, int, Expression<Func<DALCloudinaryEntity, bool>>> cloudinaryImageRepository = await GetRepositoryAsync();

            if (GetCollection(extractedEntity).Count > 1)
            {
                GetCollection(extractedEntity).Remove(dalEntity);
            }
            else
            {
                await _cloudinaryService.DeleteImageAsync(extractedEntity.PublicId);
                await cloudinaryImageRepository.DeleteEntityAsync(extractedEntity.Id);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task<string> GetReferenceAsync(TEntity entity)
        {
            TDalEntity dalEntity = await _entityService.GetDalEntityByIdAsync(entity.Id) ?? throw new ArgumentException($"Unable to get db entity of entity with id {entity.Id}");

            DALCloudinaryEntity cloudinaryEntityAttachedToEntity = GetCloudinaryEntity(dalEntity) ?? throw new ArgumentException($"There is no metadata attached to entity {entity.Id}");

            return new Uri(new Uri(_cloudinaryParameters.DefaultImageUrl), cloudinaryEntityAttachedToEntity.PublicId).AbsoluteUri;
        }

        private async Task<TDalEntity> GetDalEntityAsync(int id)
        {
            TDalEntity dalEntity = await _entityService.GetDalEntityByIdAsync(id) ?? throw new ArgumentException($"There is no entity with id {id}");

            return dalEntity;
        }
    }
}