using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class FriendList : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<FriendUser> FriendUsers { get; set; } = new List<FriendUser>();
    }
}
