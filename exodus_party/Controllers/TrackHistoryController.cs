using Microsoft.AspNetCore.Mvc;
using exodus_party.Data;
using exodus_party.Models;
using exodus_party.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using exodus_party.Hubs;

namespace exodus_party.Controllers
{
    [ApiController]
    [Route("api/[controller]")]



    public class TrackHistoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly YouTubeSearchService _youTubeSearch;
        private readonly IHubContext<PartyHub> _hubContext;
        public TrackHistoryController(AppDbContext context, YouTubeSearchService youTubeSearchService, IHubContext<PartyHub> hubContext)
        {
            _context = context;
            _youTubeSearch = youTubeSearchService;
            _hubContext = hubContext;
        }

        [HttpPost]

        public async Task<IActionResult> SaveMusic([FromBody] NewMusicRequest request)
        {
            var searchTerm = $"{request.ArtistName} {request.TrackName}";
            var videoId = await _youTubeSearch.SearchVideoIdAsync(searchTerm);
            if (string.IsNullOrEmpty(videoId))
            {
                return NotFound(new { message = "Música não encontrada no YouTube." });
            }
            var newMusic = new TrackHistory
            {
                TrackName = request.TrackName,
                ArtistName = request.ArtistName,
                YoutubeVideoId = videoId,
                PlayedAt = DateTime.UtcNow,
                PartyId = request.PartyId
            };

            _context.TrackHistories.Add(newMusic);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceberNovaMusica", newMusic);
            
            return Ok(new { message = "Música salva com sucesso no Exodus Party!", music = newMusic });
        }
        [HttpGet("atual/{partyId}")]
        public async Task<IActionResult> GetCurrentSong(int partyId)
        {
            var currentSong = await _context.TrackHistories
            .Where(t => t.PartyId == partyId)
            .OrderByDescending(t => t.Id)
            .FirstOrDefaultAsync();

            if (currentSong == null)
            {
                return NotFound(new { message = "A party ainda não começou. Nenhuma música tocando." });
            }

            return Ok(currentSong);
        }


    }
    public class NewMusicRequest
    {
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public int PartyId { get; set; }
    }
}
