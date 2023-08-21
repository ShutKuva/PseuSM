using Core.Enums;
using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public string? Description { get; set; }
        public UserStatuses Status { get; set; }

        public FollowList FollowList { get; set; } = null!;
        public FriendList FriendList { get; set; } = null!;

        public IEnumerable<Image> Images { get; set; } = new List<Image>();
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
        public IEnumerable<FollowUser> FollowUsers { get; set; } = new List<FollowUser>();
        public IEnumerable<FriendUser> FriendUsers { get; set; } = new List<FriendUser>();
    }
}
