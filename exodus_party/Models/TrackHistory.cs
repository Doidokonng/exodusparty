namespace exodus_party.Models
{
    public class TrackHistory
    {
        public int Id { get; set; }
        public string YoutubeVideoId { get; set; } = string.Empty;
        public string TrackName { get; set; } = string.Empty;
        public string ArtistName { get; set; } = string.Empty;
        public DateTime PlayedAt { get; set; }
    }
}
