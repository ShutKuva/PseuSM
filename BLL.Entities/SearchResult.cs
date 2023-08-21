namespace BLL.Entities
{
    public class SearchResult
    {
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
