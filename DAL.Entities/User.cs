using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}
