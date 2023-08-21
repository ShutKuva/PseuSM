using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class Post : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public IEnumerable<Image> Images { get; set; } = new List<Image>();
        public IEnumerable<Video> Videos { get; set; } = new List<Video>();
    }
}
