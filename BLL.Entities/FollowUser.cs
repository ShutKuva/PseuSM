using BLL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class FollowUser : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FollowListId { get; set; }
        public FollowList FollowList { get; set; } = null!;
    }
}
