using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using projekt.Models;
using System.Security.Claims;

namespace ZTPAPP.Controllers
{
    public class TestController : Controller
    {
        private readonly WDbContext _context;
        public TestController(WDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tests = await _context.Tests.ToListAsync();
            if (tests != null)
            {
                ViewBag.tests = tests;
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tests == null) return NotFound();

            var Test = await _context.Tests.Include(e => e.FlashcardSets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Test == null)
            {
                return NotFound();
            }
            ViewBag.Test = Test;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddTest()
        {
            var flashcardSets = await _context.FlashcardSets.ToListAsync();
            if (flashcardSets != null)
            {
                ViewBag.flashcardSets = flashcardSets;
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("Session ended! Sign in again");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTest(Test test, List<int> SelectedFlashcardSetsIds)
        {

            if (ModelState.IsValid)
            {
                var selectedFlashcards = _context.FlashcardSets.Where(f => SelectedFlashcardSetsIds.Contains(f.Id)).ToList();
                test.FlashcardSets = selectedFlashcards;

                _context.Tests.Add(test);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            foreach (var ad in SelectedFlashcardSetsIds)
                Console.WriteLine(ad);
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Testing(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id is needed");
            }
            var FlashcardSets = await _context.FlashcardSets.Include(e => e.Tests).Include(e => e.Flashcards).ToListAsync();
            HashSet<Flashcard> flashcards = new HashSet<Flashcard>();
            foreach (var flashcardset in FlashcardSets)
            {
                if (flashcardset.Tests.Exists(e => e.Id == id))
                {
                    foreach (var flashcard in flashcardset.Flashcards)
                    {
                        flashcards.Add(flashcard);
                    }
                }
            }
            ViewBag.testFlashcards = flashcards;
            ViewBag.randomized = flashcards.Shuffle();
            return View();
        }
        [Authorize]
        public async Task<IActionResult> SubmitTest(int? points)
        {
            ViewBag.points = points.Value;
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(e => e.Id == id);
            if(test != null)
                _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
