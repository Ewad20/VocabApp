using Microsoft.EntityFrameworkCore;
using ZTPAPP.Models;

namespace projekt.Models
{
    public class WDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<FlashcardSet> FlashcardSets { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestHistory> TestsHistories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public WDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flashcard>()
                .HasMany(e => e.FlashcardSets)
                .WithMany(e => e.Flashcards);
            modelBuilder.Entity<Test>()
                .HasMany(e => e.History)
                .WithOne(e => e.Test);
            modelBuilder.Entity<Test>()
                .HasMany(e => e.FlashcardSets)
                .WithMany(e => e.Tests);
            modelBuilder.Entity<TestHistory>()
                .HasMany(e => e.Answers)
                .WithOne(e => e.Test);
            modelBuilder.Entity<Answer>()
                .HasOne(e => e.Flashcard);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithOne(s => s.User)
                .HasForeignKey<Subscriber>(s => s.UserId);
        }
    }
}