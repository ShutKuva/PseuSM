using BLL.Entities.BaseEntities;
using Core.Enums;

namespace BLL.Entities
{
    public class ImagePlaceholder : BaseEntity
    {
        public ImageTypes Type { get; set; }
        public Image Image { get; set; } = null!;
    }
}
