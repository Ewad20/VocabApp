using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

public class FlashcardController : Controller
{
    private readonly WDbContext _context;

    public FlashcardController(WDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> AllFlashcards()
    {
        var flashcards = await _context.Flashcards.ToListAsync();
        return View(flashcards);
    }

    public IActionResult Addflashcard()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Addflashcard([Bind("SourceWord,TranslatedWord")] Flashcard flashcard)
    {
        if (ModelState.IsValid)
        {
            _context.Add(flashcard);
            await _context.SaveChangesAsync();
            return RedirectToAction("Allflashcards");
        }
        return View(flashcard);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard == null)
        {
            return NotFound();
        }
        return View(flashcard);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SourceWord,TranslatedWord")] Flashcard flashcard)
    {
        if (id != flashcard.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(flashcard);
            await _context.SaveChangesAsync();
            return RedirectToAction("Allflashcards");
        }
        return View(flashcard);
    }
    public async Task<IActionResult> Copy(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard == null)
        {
            return NotFound();
        }

        Flashcard copy = flashcard.DeepCopy();
        _context.Add(copy);
        await _context.SaveChangesAsync();

        var flashcards = await _context.Flashcards.ToListAsync();
        return RedirectToAction("AllFlashcards");
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var flashcard = await _context.Flashcards
            .FirstOrDefaultAsync(f => f.Id == id);
        if (flashcard == null)
        {
            return NotFound();
        }
        return View(flashcard);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var flashcard = await _context.Flashcards.FindAsync(id);
        if (flashcard != null)
        {
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
            return RedirectToAction("AllFlashcards");
        }
        return NotFound();
    }
}
