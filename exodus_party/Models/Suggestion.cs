using System.Text.Json.Serialization;

namespace exodus_party.Models
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public string Status { get; set; } = "Pendente";
        public DateTime SuggestedAt { get; set; }
        public int PartyId { get; set; }
        
        [JsonIgnore]
        public Party Party { get; set; }
    }
}
