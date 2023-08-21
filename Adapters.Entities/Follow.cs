namespace Adapters.Entities
{
    public class Follow
    {
        public int Id { get; set; }

        public int UserWhoFollowedId { get; set; }
        public User UserWhoFollowed { get; set; } = null!;

        public int UserWhoWasFollowedId { get; set; }
        public User UserWhoWasFollowed { get; set; } = null!;
    }
}
