using exodus_party.Models;
using Microsoft.EntityFrameworkCore;

namespace exodus_party.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TrackHistory> TrackHistories { get; set; }
        public DbSet<Party> Parties { get; set; }
    }
}
