using Microsoft.AspNetCore.Mvc;
using exodus_party.Models;
using exodus_party.Data;
using System;
using System.Threading.Tasks;

namespace exodus_party.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : Controller
    {
        private readonly AppDbContext _context;
        public PartyController (AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParty([FromBody] NewPartyRequest request)
        {
            var newParty = new Party
            {
                Name = request.Name,
                PlaylistUrl = request.PlaylistUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Parties.Add(newParty);
            await _context.SaveChangesAsync();

            return Ok(new {
                message = "Party iniciada com sucesso!",
                partyId = newParty.Id,
                partyName = newParty.Name
            });

        }
    }

    public class NewPartyRequest 
    {
        public string Name { get; set; }
        public string? PlaylistUrl { get; set; }
    }

}
