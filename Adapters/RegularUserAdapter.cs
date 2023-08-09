using Adapters.Abstractions;
using Adapters.Entities;
using AutoMapper;
using BLL.Abstractions.Services;
using Tools.Abstractions;
using BLLUser = BLL.Entities.User;

namespace Adapters
{
    public class RegularUserAdapter : IRegularUserAdapter
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IHasher _hasher;
        private readonly IMapper _mapper;

        public RegularUserAdapter(
            IUserService userService,
            IJwtService jwtService,
            IHasher hasher,
            IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<JwtToken> RegisterUserAsync(RegisterUser registerUser)
        {
            registerUser.Password = _hasher.Hash(registerUser.Password);

            BLLUser user = _mapper.Map<RegisterUser, BLLUser>(registerUser);

            await _userService.CreateUserAsync(user);

            return await FormJwtTokenAsync(user);
        }

        public async Task<JwtToken> LoginUserAsync(LoginUser loginUser)
        {
            loginUser.Password = _hasher.Hash(loginUser.Password);

            BLLUser user = await _userService.GetUserByCredentialsAsync(loginUser.Login, loginUser.Password);

            return await FormJwtTokenAsync(user);
        }

        private async Task<JwtToken> FormJwtTokenAsync(BLLUser user)
        {
            string refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);
            string accessToken = await _jwtService.GenerateAccessTokenAsync(user);

            await AttachRefreshTokenToUser(refreshToken, user);

            var result = new JwtToken()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            return result;
        }

        private Task AttachRefreshTokenToUser(string refreshToken, BLLUser user)
        {
            user.RefreshToken = refreshToken;
            return _userService.UpdateUserAsync(user);
        } 
    }
}
