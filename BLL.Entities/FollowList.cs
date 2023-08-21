using BLL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class FollowList : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<DAL.Entities.FollowUser> FollowUsers { get; set; } = new List<DAL.Entities.FollowUser>();
    }
}
