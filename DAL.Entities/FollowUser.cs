using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class FollowUser : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FollowListId { get; set; }
        public FollowList FollowList { get; set; } = null!;
    }
}
