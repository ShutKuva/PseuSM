using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;
using DALFriendList = DAL.Entities.FriendList;

namespace BLL.Services
{
    public class FriendListService : ServiceBase<FriendList, DALFriendList>, IFriendListService
    {
        public FriendListService(
            IUnitOfWork unitOfWork,
            IMapper mapper
            ) : base(unitOfWork, mapper)
        {
        }

        public Task<FriendList> CreateFriendAsync(User user)
        {
            var newFriend = new DALFriendList() { 
                UserId = user.Id,
            };

            return CreateEntityAsync(newFriend);
        }

        public async Task<FriendList> GetFriendListByUserAsync(User user)
        {
            IRepository<DALFriendList, int, Expression<Func<DALFriendList, bool>>> friendListRepository = await GetRepositoryAsync();

            IEnumerable<DALFriendList> friendListsThatMeetCondition = await friendListRepository.GetEntitiesByPredicateAsync(friendList => friendList.UserId == user.Id);

            if (!friendListsThatMeetCondition.Any())
            {
                throw new ArgumentException($"User {user.Id} has no follow list");
            }

            return _mapper.Map<FriendList>(friendListsThatMeetCondition.First());
        }
    }
}
