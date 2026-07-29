namespace exodus_party.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PlaylistUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TrackHistory> Tracks { get; set; } = new List<TrackHistory>();
        public List<Suggestion> Suggestions { get; set; } = new List<Suggestion>();
    }
}
