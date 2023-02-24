using AutoMapper;
using Bogus;
using Library.API.APIModels.Input;
using Library.API.APIModels.Output;
using Library.Data.EF.Context;
using Library.Data.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly string baseUrl;

        public BookController(IConfiguration config, LibraryContext db, IMapper mapper)
        {
            _context = db;
            _configuration = config;
            baseUrl = _configuration["baseUrl"];
            _mapper = mapper;
        }




        /*
         ### 1. Get all books. Order by provided value (title or author)
            GET https://{{baseUrl}}/api/books?order=author
            
            # Response
            # [{
            # 	"id": "number",    
            # 	"title": "string",
            # 	"author": "string",
            # 	"rating": "decimal",          	average rating
            # 	"reviewsNumber": "decimal"    	count of reviews
            # }]
         */
        [HttpGet]
        [Route("books")]
        public async Task<IActionResult> GetBooks([FromQuery] string order)
        {
            if (order != "title" && order != "author")
            {
                return BadRequest("Invalid order value");
            }
            
            var books = await _context.Books
                .Include(b => b.Ratings)
                .Include(b => b.Reviews)
                .ToListAsync();

            if (order.ToLower() == "author")
            {
                books = books.OrderBy(b => b.author).ToList();
            }
            else
            {
                books = books.OrderBy(b => b.title).ToList();
            }

            var booksResponse = books
                .Select(b => _mapper.Map<Book, BookRatingRevNumber>(b))
                .ToList();

            return Ok(booksResponse);
        }

















        /*
         ### 2. Get top 10 books with high rating and number of reviews greater than 10. You can filter books by specifying genre. Order by rating
            GET https://{{baseUrl}}/api/recommended?genre=horror
            
            # Response
            # [{
            # 	"id": "number",
            # 	"title": "string",
            # 	"author": "string",
            # 	"rating": "decimal",          	average rating
            # 	"reviewsNumber": "decimal"    	count of reviews
            # }]
         */
        [HttpGet]
        [Route("recommended")]
        public async Task<IActionResult> GetRecommended(string? genre)
        {
            var books = _context.Books
                .Include(b => b.Ratings)
                .Include(b => b.Reviews);
            
            var topBooks = books
                .Where(x=>x.Reviews.Count > 10)
                .OrderByDescending(x => x.Ratings.Average(r => r.score))
                .Take(10);

            if (genre != null) topBooks = topBooks.Where(x => x.genre == genre);

            var result = await topBooks
                .Select(b => _mapper.Map<Book, BookRatingRevNumber>(b))
                .ToListAsync();

            return Ok(result);
        }





















        /*
         ### 3. Get book details with the list of reviews
            GET https://{{baseUrl}}/api/books/{id}
            # Response
            # {
            # 	"id": "number",
            # 	"title": "string",
            # 	"author": "string",
            # 	"cover": "string",
            # 	"content": "string",
            # 	"rating": "decimal",          	average rating
            # 	"reviews": [{
            #     	    "id": "number",
            #     	    "message": "string",
            #     	    "reviewer": "string",
            # 	}]
            # }}
         */
        [HttpGet]
        [Route("books/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Books
                .Where(b => b.id == id)
                .Include(b => b.Ratings)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync();

            if (book == null) return NotFound();

            var result = _mapper.Map<Book, FullBookDetail>(book);

            return Ok(result);
        }



















        /*
         ### 4. Delete a book using a secret key. Save the secret key in the config of your application. Compare this key with a query param
        DELETE https://{{baseUrl}}/api/books/{id}?secret=qwerty
         */
        [HttpDelete]
        [Route("books/{id}")]
        public async Task<IActionResult> DeleteBook(int id, string secret)
        {
            if (secret == _configuration["secret"])
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null) return NotFound();
                else
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            else return BadRequest("Invalid secret key");
        }

















        /*
         ### 5. Save a new book.
            POST https://{{baseUrl}}/api/books/save
            
            {
            	"id": "number",             	// if id is not provided create a new book, otherwise - update an existing one
            	"title": "string",
            	"cover": "string",          	// save image as base64
            	"content": "string",
            	"genre": "string",
            	"author": "string"
            }
            
            # Response
            # {
            # 	"id": "number"
            # }        
         */
        [HttpPost]
        [Route("books/save")]
        public async Task<IActionResult> SaveBook([FromBody] BookInput book) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var bookEntity = _mapper.Map<BookInput, Book>(book);

            var bookFind = await _context.Books.FindAsync(bookEntity.id);

            if (bookFind == null)
            {
                if (book.id == 0)
                {
                    int idEntity = 0;

                    do
                    {
                        idEntity = new Faker().Random.Int();
                    }
                    while (await _context.Books.Select(x => x.id).ContainsAsync(idEntity));
                    bookEntity.id = idEntity;
                }
                _context.Books.Add(bookEntity);
                await _context.SaveChangesAsync();
                return Ok(new { id = bookEntity.id });
            }
            else
            {
                bookFind.title = bookEntity.title;
                bookFind.cover = bookEntity.cover;
                bookFind.content = bookEntity.content;
                bookFind.genre = bookEntity.genre;
                bookFind.author = bookEntity.author;
                await _context.SaveChangesAsync();
                return Ok(new { id = bookFind.id });
            }
        }




















        /*
         ### 6. Save a review for the book.
            PUT https://{{baseUrl}}/api/books/{id}/review
            
            {
            	"message": "string",
            	"reviewer": "string",
            }   
            # Response
            # {
            # 	"id": "number"
            # }
        
         */
        [HttpPut]
        [Route("books/{id}/review")]
        public async Task<IActionResult> SaveReview(int id, [FromBody] ReviewInput review) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var book = await _context.Books
                .Include(x=>x.Reviews)
                .FirstOrDefaultAsync(x => x.id == id);

            if (book == null) return NotFound();
            
            var reviewEntity = _mapper.Map<ReviewInput, Review>(review);
            
            int idEntity = 0;

            do
            {
                idEntity = new Faker().Random.Int();
            }
            while (await _context.Books.AnyAsync(x => x.Reviews.Any(y => y.id == idEntity)));
            reviewEntity.id = idEntity;
            book.Reviews.Add(reviewEntity);
            
            _context.Books.Update(book);
            return Ok();
        }















        /*
         ### 7. Rate a book
        PUT https://{{baseUrl}}/api/books/{id}/rate
        
        {
        	"score": "number",    	// score can be from 1 to 5
        }
         */
        [HttpPut]
        [Route("books/{id}/rate")]
        public async Task<IActionResult> RateBook(int id, [FromBody] RatingInput rating) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var book = await _context.Books
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.id == id);

            if (book == null) return NotFound();

            var ratingEntity = new Rating()
            {                           
                score = rating.rating,
                bookId = id
            };

            int idEntity = 0;

            do
            {
                idEntity = new Faker().Random.Int();
            }
            while (await _context.Books.AnyAsync(x => x.Ratings.Any(y => y.id == idEntity)));

            ratingEntity.id = idEntity;
            book.Ratings.Add(ratingEntity);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
