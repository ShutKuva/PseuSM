namespace Adapters.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;

        public User User { get; set; } = null!;
        public int UserId { get; set; }
    }
}
