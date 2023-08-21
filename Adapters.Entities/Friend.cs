namespace Adapters.Entities
{
    public class Friend
    {
        public int Id { get; set; }

        public int Friend1Id { get; set; }
        public User Friend1 { get; set; } = null!;

        public int Friend2Id { get; set; }
        public User Friend2 { get; set; } = null!;
    }
}
