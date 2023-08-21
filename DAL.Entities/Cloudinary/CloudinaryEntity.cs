using DAL.Entities.BaseEntities;

namespace DAL.Entities.Cloudinary
{
    public class CloudinaryEntity : BaseEntity
    {
        public string PublicId { get; set; } = string.Empty;

        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
