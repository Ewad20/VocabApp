using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using ZTPAPP.Interfaces;

public class DeepCopyStrategy : ICopyStrategy
{
    public Flashcard Copy(Flashcard original)
    {
        return original.DeepCopy();
    }
}

public class ShallowCopyStrategy : ICopyStrategy
{
    public Flashcard Copy(Flashcard original)
    {
        return original.ShallowCopy();
    }
}
public class FlashcardController : Controller
{
    private readonly WDbContext _context;
    private readonly ICopyStrategy _copyStrategy;
    public FlashcardController(WDbContext context)
    {
        _context = context;
        _copyStrategy = new DeepCopyStrategy();
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

        Flashcard copy = _copyStrategy.Copy(flashcard);
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
