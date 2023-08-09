using Adapters.Entities;

namespace Adapters.Abstractions
{
    public interface IRegularUserAdapter
    {
        Task<JwtToken> RegisterUserAsync(RegisterUser registerUser);
        Task<JwtToken> LoginUserAsync(LoginUser loginUser);
    }
}
