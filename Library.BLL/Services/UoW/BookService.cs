using Library.BLL.DTO;
using Library.Data.EF.Context;
using Library.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.UoW
{
    public class BookService
    {
        private LibraryContext db;
        public BookService(LibraryContext context)
        {
            db = context;
        }


        public async Task<Book?> GetById(int id)
        {
            var book = await db.Books.FindAsync(id);
            return book;
        }

        public async Task<List<Book>?> GetAll()
        {
            var books = await db.Books.ToListAsync();
            return books;
        }

        public async Task Add(Book book)
        {
            await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
        }
        
        public async Task Update(Book book)
        {
            db.Books.Update(book);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var book = await db.Books.FindAsync(id);
            if (book != null)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
            }
        }
    }
}
