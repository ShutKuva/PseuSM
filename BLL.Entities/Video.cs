using DAL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class Video : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;

        public User User { get; set; } = null!;
        public int UserId { get; set; }
    }
}
