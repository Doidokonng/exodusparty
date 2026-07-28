using Microsoft.AspNetCore.Mvc;
using exodus_party.Data;
using exodus_party.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace exodus_party.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TrackHistoryController : Controller
    {
        private readonly AppDbContext _context;
        public TrackHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
      
        public async Task<IActionResult> SaveMusic([FromBody] TrackHistory track)
        {
            track.PlayedAt = DateTime.UtcNow;
            _context.TrackHistories.Add(track);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Música salva com sucesso no Exodus Party!", music = track });
        }
        [HttpGet("atual")]
        public async Task<IActionResult> GetCurrentSong()
        {
            var currentSong = await _context.TrackHistories
            .OrderByDescending(t => t.Id)
            .FirstOrDefaultAsync();

            if (currentSong == null)
            {
                return NotFound(new { message = "A party ainda não começou. Nenhuma música tocando." });
            }

            return Ok(currentSong);
        }


    }
}
