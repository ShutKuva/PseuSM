using BLL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(User user);
        Task<string> GenerateRefreshTokenAsync(User user);
    }
}
