using Bogus;
using Library.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.EF.Context
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public LibraryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasKey(b => b.id);
            
            modelBuilder.Entity<Rating>()
                .HasKey(r => r.id);

            modelBuilder.Entity<Review>()
                .HasKey(r => r.id);


            // Seed data
            //modelBuilder.Entity<Book>().HasData(new Faker<Book>()
            //    .RuleFor(b => b.id, f => f.Random.Int())
            //    .RuleFor(b => b.author, f => f.Person.FullName)
            //    .RuleFor(b => b.title, f => f.Lorem.Sentence(3))
            //    .RuleFor(b => b.cover, f => f.Image.PicsumUrl())
            //    .RuleFor(b => b.content, f => f.Lorem.Paragraphs(3))
            //    .RuleFor(b => b.genre, f => f.PickRandom("Fantasy", "Horror", "Romance", "Sci-Fi", "Thriller"))
            //    .Generate(50));





            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.bookId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Ratings)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.bookId);
        }
    }
}
