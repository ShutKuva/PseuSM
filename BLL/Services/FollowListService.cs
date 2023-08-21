using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALFollowList = DAL.Entities.FollowList;

namespace BLL.Services
{
    public class FollowListService : ServiceBase<FollowList, DALFollowList>, IFollowListService
    {
        public FollowListService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
        }

        public Task<FollowList> CreateFollowListAsync(User user)
        {
            var newFollow = new DALFollowList()
            {
                UserId = user.Id,
            };

            return CreateEntityAsync(newFollow);
        }

        public async Task<FollowList> GetFollowListByUserAsync(User user)
        {
            IRepository<DALFollowList, int, Expression<Func<DALFollowList, bool>>> followListRepository = await GetRepositoryAsync();

            IEnumerable<DALFollowList> followListsThatMeetCondition = await followListRepository.GetEntitiesByPredicateAsync(followList => followList.UserId == user.Id);

            if (!followListsThatMeetCondition.Any())
            {
                throw new ArgumentException($"User {user.Id} has no follow list");
            }

            return _mapper.Map<FollowList>(followListsThatMeetCondition.First());
        }
    }
}
