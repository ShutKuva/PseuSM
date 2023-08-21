namespace Adapters.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
