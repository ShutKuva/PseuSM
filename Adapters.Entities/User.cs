using Core.Enums;

namespace Adapters.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public UserStatuses Status { get; set; } 

        public IEnumerable<Image> Images { get; set; } = new List<Image>();
        public IEnumerable<Follow> Follows { get; set; } = new List<Follow>();
        public IEnumerable<Friend> Friends { get; set; } = new List<Friend>();
    }
}
