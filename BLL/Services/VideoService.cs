using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.MappedAbstractions;
using BLL.Parameters;
using BLL.Services.BaseServices;
using DAL.Abstractions.UnitOfWork;
using DALVideo = DAL.Entities.Video;

namespace BLL.Services
{
    public class VideoService : ServiceBase<Video, DALVideo>
    {
        private readonly IEntityMetadataService<Video, DALVideo, CloudinaryParameters> _metadataService;

        public VideoService(
            ICloudinaryMetadataService<Video, DALVideo> metadataService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
            _metadataService = metadataService;
        }

        public Task<Video> CreateVideoAsync(Video video, Post post)
        {
            DALVideo newVideo = _mapper.Map<DALVideo>(video);

            return CreateEntityAsync(newVideo);
        }

        public async Task<Video> CreateVideoFromStreamAsync(Stream videoStream, string videoName, Post post)
        {
            var newVideo = new DALVideo
            {
                Name = videoName,
                PostId = post.Id,
            };
            Video mappedVideo = await CreateEntityAsync(newVideo);

            await _metadataService.CreateMetadataFromStreamAsync(videoStream, mappedVideo, new CloudinaryParameters { Post = post });

            mappedVideo.Reference = newVideo.Reference = await _metadataService.GetReferenceAsync(mappedVideo);
            await _unitOfWork.CommitAsync();

            return mappedVideo;
        }

        public async Task DeleteVideoAsync(Video video)
        {
            await _metadataService.DeleteMetadataAsync(video);
            await DeleteEntityAsync(video.Id); 
        }
    }
}
