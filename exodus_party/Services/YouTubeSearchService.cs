using System.Text.Json;
using exodus_party.DTOs;

namespace exodus_party.Services
{
    public class YouTubeSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public YouTubeSearchService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["YouTube:ApiKey"];
        }

        public async Task<string> SearchVideoIdAsync(string nameTrack)
        {
            var url = $"https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults=1&q={nameTrack}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dados = JsonSerializer.Deserialize<YouTubeSearchResponse>(jsonString, options);
            return dados?.Items?.FirstOrDefault()?.Id?.VideoId;
        }

    }
}
