using Core.Enums;
using DAL.Entities.BaseEntities;
using DAL.Entities.Cloudinary;

namespace DAL.Entities
{
    public class Image : BaseEntity
    {
        public string? Reference { get; set; } = string.Empty;
        public ImageTypes Type { get; set; } = ImageTypes.Regular;

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
        public ImagePlaceholder? ImagePlaceholder { get; set; } = null!;
        public int? PostId { get; set; }
        public Post? Post { get; set; } = null!;
        public int? CloudinaryEntityId { get; set; }
        public CloudinaryEntity? CloudinaryEntity { get; set; } = null!;
    }
}
