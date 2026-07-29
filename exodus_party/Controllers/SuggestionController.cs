using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Linq;
using exodus_party.Models;
using exodus_party.Data;
using exodus_party.Hubs;

namespace exodus_party.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<PartyHub> _hubContext;
        
        public SuggestionController (AppDbContext context, IHubContext<PartyHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuggestion([FromBody] NewSuggestionRequest request)
        {
            var duplicate = await _context.Suggestions
                .FirstOrDefaultAsync(s =>
                s.PartyId == request.PartyId &&
                s.TrackName.ToLower() == request.TrackName.ToLower() &&
                s.ArtistName.ToLower() == request.ArtistName.ToLower());

            if (duplicate != null)
            {
                return BadRequest(new { message = "Essa música já foi indicada! Sugira outra. " });
            }

            var newSuggestion = new Suggestion 
            {
                TrackName = request.TrackName,
                ArtistName = request.ArtistName,
                PartyId = request.PartyId,
                Status = "Pendente",
                SuggestedAt = DateTime.UtcNow
            };

            _context.Suggestions.Add(newSuggestion);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceberNovaIndicacao", newSuggestion);

            return Ok(new { message = "Indicação enviada com sucesso para o Host!" });
        }

        [HttpGet("pendentes{partyId}")]
        public async Task<IActionResult> GetPendingSuggestion(int partyId)
        {
            var pendentes = await _context.Suggestions
                .Where(s => s.PartyId == partyId && s.Status == "Pendente")
                .OrderBy(s => s.SuggestedAt)
                .ToListAsync();
            return Ok(pendentes);
        }

    }

    public class NewSuggestionRequest 
    {
        public string TrackName { get; set; }
        public string ArtistName { get; set; }
        public int PartyId { get; set; }
    }
}
