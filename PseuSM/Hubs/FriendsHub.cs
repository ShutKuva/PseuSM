using Adapters.Abstractions;
using AutoMapper;
using Core.Enums;
using Microsoft.AspNetCore.SignalR;
using PseuSM.Entities;
using AdapterUser = Adapters.Entities.User;

namespace PseuSM.Hubs
{
    public class FriendsHub : Hub
    {
        private const string GET_FRIENDS = "getFriends";
        private const string UPDATE_USER = "updateUser";

        private readonly IFriendsAdapter _friendsAdapter;
        private readonly IUserAdapter _userAdapter;
        private readonly IMapper _mapper;

        public FriendsHub(
            IFriendsAdapter friendsAdapter,
            IUserAdapter userAdapter,
            IMapper mapper
            )
        {
            _friendsAdapter = friendsAdapter;
            _userAdapter = userAdapter;
            _mapper = mapper;
        }

        public async Task GetFriends(int id, FriendsFilters friendsFilter)
        {
            IEnumerable<User> friends = _mapper.Map<IEnumerable<User>>(await _friendsAdapter.GetFriendsAsync(new AdapterUser() { Id = id }, friendsFilter));

            foreach (User user in friends)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, user.Id.ToString());
            }

            await Clients.Group(id.ToString()).SendAsync(GET_FRIENDS, friends);
        }

        public async Task UpdateUser(int id)
        {
            User user = _mapper.Map<User>(await _userAdapter.GetUserByIdAsync(id));

            await Clients.Group(id.ToString()).SendAsync(UPDATE_USER, user);
        }

        public async Task UpdateSubscriptionToFriendsGroup(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, user.Id.ToString());
            }
        }
    }
}
