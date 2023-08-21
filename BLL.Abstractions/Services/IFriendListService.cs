using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IFriendListService
    {
        Task<FriendList> CreateFriendAsync(User user);
        Task<FriendList> GetFriendListByUserAsync(User user);
    }
}
