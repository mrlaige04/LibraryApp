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


            modelBuilder.Entity<Book>().HasData(
                new Book { id = 1, title = "To Kill a Mockingbird", content = "Lorem ipsum...", author = "Harper Lee", genre = "novel", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 2, title = "1984", content = "Lorem ipsum...", author = "George Orwell", genre = "classic", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 3, title = "Pride and Prejudice", content = "Lorem ipsum...", author = "Jane Austen", genre = "romance", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 4, title = "The Great Gatsby", content = "Lorem ipsum...", author = "F. Scott Fitzgerald", genre = "novel ", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 5, title = "One Hundred Years of Solitude", content = "Lorem ipsum...", author = "Gabriel Garcia Marquez", genre = "novel", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 6, title = "The Catcher in the Rye", content = "Lorem ipsum...", author = "J.D. Salinger", genre = "novel", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 7, title = "Dracula", content = "Lorem ipsum...", author = "Bram Stoker", genre = "horror", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 8, title = "The Silence of the Lambs", content = "Lorem ipsum...", author = "Thomas Harris", genre = "horror", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 9, title = "Rosemary's Baby", content = "Lorem ipsum...", author = "Ira Levin", genre = "horror", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 10,title = "The Haunting of Hill House", content = "Lorem ipsum...", author = "Shirley Jackson", genre = "horror", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." },
                new Book { id = 11,title = "The Turn of the Screw", content = "Lorem ipsum...", author = "Henry James", genre = "horror", cover = "data:image/png;base64,iVBORw0KGg+rR9wo1b1l/F63P..." }
                );

            modelBuilder.Entity<Review>().HasData(
                new Review { id = 1,    message = "Lorem ipsum...", bookId = 1, reviewer = "Ayrton Whitehead" },
                new Review { id = 2,    message = "Lorem ipsum...", bookId = 1, reviewer = "Madiha Mccann" },
                new Review { id = 3,    message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 4,    message = "Lorem ipsum...", bookId = 2, reviewer = "Ayrton Whitehead" },
                new Review { id = 5,    message = "Lorem ipsum...", bookId = 2, reviewer = "Rihanna Woodward" },
                new Review { id = 6,    message = "Lorem ipsum...", bookId = 3, reviewer = "Ronald Anderson" },
                new Review { id = 7,    message = "Lorem ipsum...", bookId = 4, reviewer = "Kabir Fisher" },
                new Review { id = 8,    message = "Lorem ipsum...", bookId = 5, reviewer = "Ronald Anderson" },
                new Review { id = 9,    message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 10,   message = "Lorem ipsum...", bookId = 1, reviewer = "Kabir Fisher" },
                new Review { id = 11,   message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 12,   message = "Lorem ipsum...", bookId = 1, reviewer = "Marwa Ponce" },
                new Review { id = 13,   message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 14,   message = "Lorem ipsum...", bookId = 1, reviewer = "Ronald Anderson" },
                new Review { id = 15,   message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 16,   message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" },
                new Review { id = 17,   message = "Lorem ipsum...", bookId = 1, reviewer = "Jeremy Campos" },
                new Review { id = 18,   message = "Lorem ipsum...", bookId = 1, reviewer = "Rihanna Woodward" }
                );

            modelBuilder.Entity<Rating>().HasData(
                new Rating { id = 1, bookId = 1, score = 5 },
                new Rating { id = 2, bookId = 1, score = 4 },
                new Rating { id = 3, bookId = 1, score = 4 },
                new Rating { id = 4, bookId = 2, score = 3 },
                new Rating { id = 5, bookId = 2, score = 5 },
                new Rating { id = 6, bookId = 3, score = 2 },
                new Rating { id = 7, bookId = 4, score = 4 },
                new Rating { id = 8, bookId = 5, score = 3 }
                );




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
