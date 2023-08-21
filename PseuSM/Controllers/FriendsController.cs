using Adapters.Abstractions;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PseuSM.Entities;
using PseuSM.Models;
using AdapterUser = Adapters.Entities.User; 

namespace PseuSM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsAdapter _friendAdapter;
        private readonly IMapper _mapper;

        public FriendsController(IFriendsAdapter friendAdapter, IMapper mapper)
        {
            _friendAdapter = friendAdapter;
            _mapper = mapper;
        }

        [HttpPost("follow")]
        public async Task<OkResult> FollowUser(UserOnlyId userToFollow)
        {
            await _friendAdapter.FollowAsync(GetCurrentUser(), _mapper.Map<AdapterUser>(userToFollow));

            return Ok();
        }

        [HttpPost("unfollow")]
        public async Task<OkResult> UnFollowUser(UserOnlyId userToUnfollow)
        {
            await _friendAdapter.UnFollowAsync(GetCurrentUser(), _mapper.Map<AdapterUser>(userToUnfollow));

            return Ok();
        }

        private AdapterUser GetCurrentUser()
        {
            int id = int.Parse(User.FindFirst(UserClaimsConstants.Id)?.Value ?? throw new ArgumentException("Unable to get id"));

            var currentUser = new AdapterUser()
            {
                Id = id,
            };

            return currentUser;
        }
    }
}
