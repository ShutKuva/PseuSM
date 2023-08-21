using BLL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class FriendList : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<DAL.Entities.FriendUser> FriendUsers { get; set; } = new List<DAL.Entities.FriendUser>();
    }
}
