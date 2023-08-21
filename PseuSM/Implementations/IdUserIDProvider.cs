using Core;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace PseuSM.Implementations
{
    public class IdUserIDProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirstValue(UserClaimsConstants.Id);
        }
    }
}
