using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IFriendService
    {
        Task<FriendUser> CreateFriendAsync(User friend1, User friend2);
        Task DeleteFriendAsync(User friend1, User friend2);
    }
}
