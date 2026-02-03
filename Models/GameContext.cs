using Microsoft.EntityFrameworkCore;

namespace VideoGameBacklogTracker.Models
{
    // The parameters are now right next to the class name!
    public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
    {
        public DbSet<Game> Games { get; set; }
    }
}