using BLL.Entities;
using Core.Enums;

namespace BLL.Services.Parameters
{
    public class CloudinaryImageParameters
    {
        public User User { get; set; } = null!;
        public ImageTypes Type { get; set; } = ImageTypes.Regular;
    }
}
