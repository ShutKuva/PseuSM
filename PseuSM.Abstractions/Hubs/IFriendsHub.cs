using Core.Enums;
using PseuSM.Entities;

namespace PseuSM.Abstractions.Hubs
{
    public interface IFriendsHub
    {
        Task GetFriends(FriendsFilters friendsFilter);
        Task UpdateUser(int id);
        Task UpdateSubscriptionToFriendsGroup(IEnumerable<User> users);
    }
}
