using BLL.Entities.BaseEntities;
using Core.Enums;

namespace BLL.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public UserStatuses Status { get; set; }

        public FollowList FollowList { get; set; } = null!;
        public FriendList FriendList { get; set; } = null!;
        public IEnumerable<Image> Images { get; set; } = new List<Image>();
    }
}
