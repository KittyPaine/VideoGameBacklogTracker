using System.ComponentModel.DataAnnotations;

namespace VideoGameBacklogTracker.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Platform { get; set; } = string.Empty;

        // This line adds the space in your table headers and forms
        [Display(Name = "Completion Percentage")]
        [Range(0, 100)]
        public int CompletionPercentage { get; set; }
    }
}