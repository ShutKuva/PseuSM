using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class FollowList : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<FollowUser> FollowUsers { get; set; } = new List<FollowUser>();
    }
}
