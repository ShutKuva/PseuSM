namespace Adapters.Entities
{
    public class SearchResult
    {
        public IEnumerable<User> Users { get; set; } = new HashSet<User>();
        public IEnumerable<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
