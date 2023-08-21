namespace Core
{
    public class ConfigurationsForFakeData
    {
        public bool FakeData { get; set; }
        public string Password { get; set; } = string.Empty;
        public string CloudianryImageUrl { get; set; } = string.Empty;
        public string CloudianryVideoUrl { get; set; } = string.Empty;
        public IEnumerable<string> AvatarPublicIds { get; set; } = new List<string>();
        public IEnumerable<string> VideoPublicIds { get; set; } = new List<string>();
        public IEnumerable<string> BackgroundPublicIds { get; set; } = new List<string>();
        public IEnumerable<string> ImagePublicIds { get; set; } = new List<string>(); 
    }
}
