using BLL.Entities.BaseEntities;
using Core.Enums;

namespace BLL.Entities
{
    public class Image : BaseEntity
    {
        public string Reference { get; set; } = string.Empty;
        public ImageTypes Type { get; set; } = ImageTypes.Regular;
    }
}
