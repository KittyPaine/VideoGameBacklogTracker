using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideoGameBacklogTracker.Models;

namespace VideoGameBacklogTracker.Controllers
{
    // All methods must live inside this class to avoid CS0106
    public class GamesController(GameContext context) : Controller
    {
        // 1. INDEX PAGE (Search & Platform Filter)
        public async Task<IActionResult> Index(string gamePlatform, string searchString)
        {
            IQueryable<string> platformQuery = from g in context.Games
                                               orderby g.Platform
                                               select g.Platform;

            var games = from g in context.Games
                        select g;

            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(gamePlatform))
            {
                games = games.Where(x => x.Platform == gamePlatform);
            }

            var gameVM = new GameIndexViewModel
            {
                Platforms = new SelectList(await platformQuery.Distinct().ToListAsync()),
                Games = await games.ToListAsync(),
                SearchString = searchString,
                GamePlatform = gamePlatform
            };

            return View(gameVM);
        }

        // 2. CREATE (GET)
        [HttpGet]
        public IActionResult Create() => View();

        // 3. CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Title,Platform,CompletionPercentage")] Game game)
        {
            if (ModelState.IsValid)
            {
                context.Add(game);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // 4. EDIT (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var game = await context.Games.FindAsync(id);
            if (game == null) return NotFound();
            return View(game);
        }

        // 5. EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,Title,Platform,CompletionPercentage")] Game game)
        {
            if (id != game.GameId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(game);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Games.Any(e => e.GameId == game.GameId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // 6. DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var game = await context.Games.FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null) return NotFound();
            return View(game);
        }

        // 7. DELETE (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var game = await context.Games.FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null) return NotFound();
            return View(game);
        }

        // 8. DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await context.Games.FindAsync(id);
            if (game != null)
            {
                context.Games.Remove(game);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}