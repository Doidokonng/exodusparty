using Microsoft.AspNetCore.Mvc;
using exodus_party.Data;
using exodus_party.Models;

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
        public IActionResult SaveMusic([FromBody] TrackHistory track)
        {
            _context.TrackHistories.Add(track);
            _context.SaveChanges();
            return Ok(new { message = "Música salva com sucesso no Exodus Party!", music = track });
        }
    }
}
