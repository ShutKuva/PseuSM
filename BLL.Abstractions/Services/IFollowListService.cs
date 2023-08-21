using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IFollowListService
    {
        Task<FollowList> CreateFollowListAsync(User user);
        Task<FollowList> GetFollowListByUserAsync(User user);
    }
}
