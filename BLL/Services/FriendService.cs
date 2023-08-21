using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.UnitOfWork;
using DALFriendUser = DAL.Entities.FriendUser;

namespace BLL.Services
{
    public class FriendService : ServiceBase<FriendUser, DALFriendUser>, IFriendService
    {
        private readonly IFriendListService _friendListService;

        public FriendService(
            IFriendListService friendListService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
            _friendListService = friendListService;
        }

        public async Task<FriendUser> CreateFriendAsync(User friend1, User friend2)
        {
            FriendList friendList1 = await _friendListService.GetFriendListByUserAsync(friend1);
            FriendList friendList2 = await _friendListService.GetFriendListByUserAsync(friend2);

            var friendUser1 = new DALFriendUser
            {
                FriendListId = friendList1.Id,
                UserId = friend2.Id,
            };

            var friendUser2 = new DALFriendUser
            {
                FriendListId = friendList2.Id,
                UserId = friend1.Id,
            };

            await CreateEntityAsync(friendUser2);

            return await CreateEntityAsync(friendUser1);
        }

        public async Task DeleteFriendAsync(User friend1, User friend2)
        {
            FriendList friendList1 = await _friendListService.GetFriendListByUserAsync(friend1);
            FriendList friendList2 = await _friendListService.GetFriendListByUserAsync(friend2);

            await DeleteEntityAsync(null, friendUser => friendUser.FriendListId == friendList1.Id && friendUser.UserId == friend2.Id, $"User {friendList1.UserId} and user {friend2.Id} are not friends");
            await DeleteEntityAsync(null, friendUser => friendUser.FriendListId == friendList2.Id && friendUser.UserId == friend1.Id, $"User {friendList2.UserId} and user {friend1.Id} are not friends");
        }
    }
}
