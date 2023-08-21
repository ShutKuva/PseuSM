using Core.Enums;
using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class ImagePlaceholder : BaseEntity
    {
        public ImageTypes Type { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; } = null!;
    }
}
