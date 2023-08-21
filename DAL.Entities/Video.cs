using DAL.Entities.BaseEntities;
using DAL.Entities.Cloudinary;

namespace DAL.Entities
{
    public class Video : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
        public int? PostId { get; set; }
        public Post? Post { get; set; } = null!;
        public int? CloudinaryEntityId { get; set; }
        public CloudinaryEntity? CloudinaryEntity { get; set; } = null!;
    }
}
