using BLL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class Post : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
