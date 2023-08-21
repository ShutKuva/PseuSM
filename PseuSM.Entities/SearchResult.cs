namespace PseuSM.Entities
{
    public class SearchResult
    {
        public IEnumerable<UserPreview> Users { get; set; } = new List<UserPreview>();
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();
    }
}
