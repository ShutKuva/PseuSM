using Adapters.Abstractions;
using Adapters.Entities;
using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Abstractions.Tools;
using BLL.Parameters;
using Core.Enums;
using BLLUser = BLL.Entities.User;

namespace Adapters
{
    public class FriendsAdapter : IFriendsAdapter
    {
        private readonly IUserService<GetUserParams> _userService;
        private readonly IFollowService _followService;
        private readonly IFriendService _friendService;
        private readonly IParamsResolver<GetUserParams, GetFriendParamsResolverParams> _friendParamsResolver;
        private readonly IMapper _mapper;

        public FriendsAdapter(
            IUserService<GetUserParams> userService,
            IFollowService followService, 
            IFriendService friendService,
            IParamsResolver<GetUserParams, GetFriendParamsResolverParams> friendParamsResolver,
            IMapper mapper
            )
        {
            _userService = userService;
            _followService = followService;
            _friendService = friendService;
            _friendParamsResolver = friendParamsResolver;
            _mapper = mapper;
        }

        public async Task FollowAsync(User current, User followedUser)
        {
            BLLUser mappedCurrent = _mapper.Map<BLLUser>(current);
            BLLUser mappedFollowedUser = _mapper.Map<BLLUser>(followedUser);

            await _followService.CreateFollowAsync(mappedCurrent, mappedFollowedUser);

            mappedFollowedUser = await _userService.GetUserByIdAsync(followedUser.Id);

            if (mappedFollowedUser.FriendList.FriendUsers.Any(followUser => followUser.User.Id == current.Id))
            {
                await _friendService.CreateFriendAsync(_mapper.Map<BLLUser>(current), mappedFollowedUser);
            }
        }

        public async Task UnFollowAsync(User current, User followedUser)
        {
            BLLUser mappedCurrent = _mapper.Map<BLLUser>(current);
            BLLUser mappedFollowedUser = _mapper.Map<BLLUser>(followedUser);

            await _followService.DeleteFollowAsync(mappedCurrent, mappedFollowedUser);
            await _friendService.DeleteFriendAsync(mappedCurrent, mappedFollowedUser);
        }

        public async Task<IEnumerable<User>> GetFriendsAsync(User user, FriendsFilters friendsFilter)
        {
            return _mapper.Map<IEnumerable<User>>(await _userService.GetEntitiesByPredicateAsync(await _friendParamsResolver.CreateParamsAsync(new GetFriendParamsResolverParams() {
                Filter = friendsFilter,
                UserId = user.Id,
            })));
        }
    }
}
