using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Controllers
{
    public class FlashcardSetController : Controller
    {
        private readonly WDbContext _context;

        public FlashcardSetController(WDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.FlashcardSets != null ? 
                          View(await _context.FlashcardSets.ToListAsync()) :
                          Problem("Entity set 'WDbContext.FlashcardSets'  is null.");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FlashcardSets == null) return NotFound();

            var flashcardSet = await _context.FlashcardSets.Include(e=>e.Flashcards)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flashcardSet == null)
            {
                return NotFound();
            }

            return View(flashcardSet);
        }

        public IActionResult Create()
        {
            List<Flashcard> Flashcards = _context.Flashcards.ToList();
            ViewBag.Flashcards = Flashcards;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlashcardSet flashcardSet, List<int> SelectedFlashcardIds)
        {
            if (ModelState.IsValid)
            {
                var selectedFlashcards = _context.Flashcards.Where(f => SelectedFlashcardIds.Contains(f.Id)).ToList();
                flashcardSet.Flashcards = selectedFlashcards;

                _context.Add(flashcardSet);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(flashcardSet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FlashcardSets == null)
            {
                return NotFound();
            }

            var flashcardSet = await _context.FlashcardSets.Include(f => f.Flashcards).FirstOrDefaultAsync(m => m.Id == id);
            if (flashcardSet == null)
            {
                return NotFound();
            }

            List<Flashcard> allFlashcards = _context.Flashcards.ToList();
            List<int> selectedFlashcardIds = flashcardSet.Flashcards.Select(f => f.Id).ToList();

            var flashcardsViewModel = allFlashcards.Select(f => new
            {
                Flashcard = f,
                IsSelected = selectedFlashcardIds.Contains(f.Id)
            });

            ViewBag.Flashcards = flashcardsViewModel;

            return View(flashcardSet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FlashcardSet flashcardSetModel, List<int> SelectedFlashcardIds)
        {
            if (id != flashcardSetModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingSet = await _context.FlashcardSets.Include(f => f.Flashcards).FirstOrDefaultAsync(f => f.Id == id);

                    if (existingSet != null)
                    {
                        existingSet.Name = flashcardSetModel.Name;

                        existingSet.Flashcards.Clear();
                        var selectedFlashcards = await _context.Flashcards.Where(f => SelectedFlashcardIds.Contains(f.Id)).ToListAsync();
                        existingSet.Flashcards = selectedFlashcards;

                        _context.Update(existingSet);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlashcardSetExists(flashcardSetModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flashcardSetModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FlashcardSets == null)
            {
                return NotFound();
            }

            var flashcardSet = await _context.FlashcardSets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flashcardSet == null)
            {
                return NotFound();
            }

            return View(flashcardSet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FlashcardSets == null) return Problem("Entity set 'WDbContext.FlashcardSets'  is null.");

            var flashcardSet = await _context.FlashcardSets.FindAsync(id);

            if (flashcardSet != null) _context.FlashcardSets.Remove(flashcardSet);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlashcardSetExists(int id)
        {
          return (_context.FlashcardSets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
