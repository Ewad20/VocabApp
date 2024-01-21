using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace projekt.Models
{
    public class WDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardSet> FlashcardSets { get; set; }
        public WDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flashcard>()
                .HasMany(e => e.FlashcardSets)
                .WithMany(e => e.Flashcards);
        }
    }
}