using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Abstractions.Tools;
using BLL.Entities;
using BLL.MappedAbstractions;
using BLL.Parameters;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using Tools.Abstractions;
using DALPost = DAL.Entities.Post;

namespace BLL.Services
{
    public class PostService : ServiceBase<Post, DALPost>, IPostService<GetPostsParams>, IProGetterService<Post, DALPost>
    {
        private readonly ITimeService _timeService;

        public PostService(
            ITimeService timeService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
            _timeService = timeService;
        }

        public async Task<Post> CreatePost(Post newPost)
        {
            newPost.UpdateDate = await _timeService.GetCurrentDateTime();
            newPost.UpdateDate = await _timeService.GetCurrentDateTime();

            return await CreateEntityAsync(_mapper.Map<DALPost>(newPost));
        }

        public async Task<Post> CreatePost(string description, User user)
        {
            DateTime now = await _timeService.GetCurrentDateTime();

            var newPost = new DALPost
            {
                Description = description,
                UserId = user.Id,
                CreationDate = now,
                UpdateDate = now,
            };

            return await CreateEntityAsync(_mapper.Map<DALPost>(newPost));
        }

        public Task DeletePost(Post deletePost)
        {
            return DeleteEntityAsync(deletePost.Id);
        }

        public Task<IEnumerable<Post>> GetEntitiesByPredicateAsync(GetPostsParams parameters)
        {
            return GetPostsThatFitPredicateAsync(parameters.Predicate);
        }

        public Task<IEnumerable<Post>> GetEntitiesByPredicateAsync(Expression<Func<DALPost, bool>> predicate)
        {
            return GetPostsThatFitPredicateAsync(predicate);
        }

        private async Task<IEnumerable<Post>> GetPostsThatFitPredicateAsync(Expression<Func<DALPost, bool>> predicate)
        {
            IRepository<DALPost, int, Expression<Func<DALPost, bool>>> postsRepository = await GetRepositoryAsync();
            return _mapper.Map<IEnumerable<Post>>(await postsRepository.GetEntitiesByPredicateAsync(predicate));
        }
    }
}
