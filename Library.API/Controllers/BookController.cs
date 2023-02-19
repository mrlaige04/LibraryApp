using Library.Data.EF.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration config, LibraryContext db)
        {
            _context = db;
            _configuration = config;
        }

        [HttpGet("GetAll")]
        [Route("")]
        public async Task<IActionResult> GetAll(string order)
        {
            if (order == "title") return Ok(await _context.Books.OrderBy(b => b.title).ToListAsync());
            if (order == "author") return Ok(await _context.Books.OrderBy(b => b.author).ToListAsync());
            else return BadRequest("Invalid order parameter");
        }
    }
}
