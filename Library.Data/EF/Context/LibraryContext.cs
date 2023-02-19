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
            
            
        }
    }
}
