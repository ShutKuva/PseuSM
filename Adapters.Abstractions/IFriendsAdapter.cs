using Adapters.Entities;
using Core.Enums;

namespace Adapters.Abstractions
{
    public interface IFriendsAdapter
    {
        Task FollowAsync(User current, User followedUser);

        Task UnFollowAsync(User current, User followedUser);

        Task<IEnumerable<User>> GetFriendsAsync(User user, FriendsFilters friendsFilter);
    }
}
