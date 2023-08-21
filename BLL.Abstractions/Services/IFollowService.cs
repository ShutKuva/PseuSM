using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IFollowService
    {
        Task<FollowUser> CreateFollowAsync(User userWhoFollows, User userWhoWasFollowed);
        Task DeleteFollowAsync(User userWhoFollows, User userWhoWasFollowed);
    }
}
