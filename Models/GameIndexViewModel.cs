using Microsoft.AspNetCore.Mvc.Rendering;

namespace VideoGameBacklogTracker.Models
{
    public class GameIndexViewModel
    {
        public List<Game>? Games { get; set; }
        public SelectList? Platforms { get; set; }
        public string? GamePlatform { get; set; }
        public string? SearchString { get; set; }
    }
}