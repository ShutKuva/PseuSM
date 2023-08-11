using DAL.Entities.BaseEntities;

namespace DAL.Entities.Cloudinary
{
    public class CloudinaryImage : BaseEntity
    {
        public string PublicId { get; set; } = string.Empty;

        public int ImageId { get; set; }
        public Image Image { get; set; } = null!;
    }
}
