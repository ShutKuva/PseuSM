using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.UnitOfWork;
using DALFollowUser = DAL.Entities.FollowUser;

namespace BLL.Services
{
    public class FollowService : ServiceBase<FollowUser, DALFollowUser>, IFollowService
    {
        private readonly IFollowListService _followListService;

        public FollowService(
            IUnitOfWork unitOfWork,
            IFollowListService followListService,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
            _followListService = followListService;
        }

        public async Task<FollowUser> CreateFollowAsync(User userWhoFollows, User userWhoWasFollowed)
        {
            FollowList followList = await _followListService.GetFollowListByUserAsync(userWhoFollows);

            var newFollowUser = new DALFollowUser
            {
                FollowListId = followList.Id,
                UserId = userWhoWasFollowed.Id,
            };

            return await CreateEntityAsync(newFollowUser);
        }

        public async Task DeleteFollowAsync(User userWhoFollows, User userWhoWasFollowed)
        {
            FollowList followList = await _followListService.GetFollowListByUserAsync(userWhoFollows);

            await DeleteEntityAsync(null, followUser => followUser.FollowListId == followList.Id && followUser.UserId == userWhoWasFollowed.Id, $"User {followList.UserId} does not follow user {userWhoWasFollowed.Id}");
        }
    }
}
