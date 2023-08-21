using Core.Enums;

namespace BLL.Parameters
{
    public class GetFriendParamsResolverParams
    {
        public int UserId { get; set; }
        public FriendsFilters Filter { get; set; } = FriendsFilters.All;
    }
}
