using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Management.Models;
using Microsoft.AspNetCore.Identity;
using Management.Data;
using Microsoft.EntityFrameworkCore;
using Humanizer.Localisation;
using static System.Reflection.Metadata.BlobBuilder;
using Management.ViewModel;

namespace Management.Controllers
{
    [ApiController]
    [Route("book")]
    public class ManageBook : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ManageBook(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("/book/viewtitle")]
        [HttpGet]
        public async Task<IActionResult> sershsearch()
        {
            var books = await _context.Books.FromSqlInterpolated($"SELECT * FROM [security].Books ").OrderByDescending(b => b.average_rating).ToListAsync();
            return Ok(books);
        }

        [Route("/book/viewcategorie")]
        [HttpGet]
        public async Task<IActionResult> sershcategorie()
        {
            var books = await _context.Books.FromSqlInterpolated($"select DISTINCT categories from [security].[Books] ").Select(b => new { Categories = b.categories }).ToListAsync();
            return Ok(books);
        }

        [Route("/book/title/{title}")]
        [HttpGet]
        public async Task<IActionResult> sershsearch(string? title)
        {
            List<book> books = new List<book>();

            if (!string.IsNullOrEmpty(title))
            {
                books = await _context.Books.FromSqlInterpolated($"SELECT * FROM [security].Books WHERE title LIKE {"%" + title + "%"}").OrderByDescending(b => b.average_rating).ToListAsync();
                if (books.Count == 0)
                    return BadRequest($"incorrect title = {title}");
            }
            else
            {
                books = await _context.Books.FromSqlInterpolated($"SELECT * FROM [security].Books ").OrderByDescending(b => b.average_rating).ToListAsync();

            }
            return Ok(books);

        }
                
        [Route("/book/categorie/{categories}")]
        [HttpGet]
        public async Task<IActionResult> sershcategorie(string categories)
        {
            if (string.IsNullOrEmpty(categories))
            {
                return BadRequest("Invalid category parameter.");
            }
            List<book> books = new List<book>();

            books = await _context.Books.FromSqlInterpolated($"SELECT * FROM [security].Books WHERE categories LIKE {"%" + categories + "%"}").OrderByDescending(b => b.average_rating).ToListAsync();
            if (books.Count == 0)
                return BadRequest($"incorrect title = {categories}");

           

            return Ok(books);
        }

        [Authorize(Roles = "Admin")]
        [Route("/book/add")]
        [HttpPost] //input data
        public async Task<IActionResult> add([FromForm] BookViewModel b)
        {
            if(b == null) { return BadRequest("where your book??"); }
            if (!ModelState.IsValid)
            {
                return BadRequest(b);
            }

            var book = new book {
                title = b.title,
                categories=b.categories,
                authors=b.authors,
                description=b.description,
                published_year=b.published_year,
                average_rating=b.average_rating,
                ratings_count=b.ratings_count,
                num_pages=b.num_pages,
                url_image=b.url_image
            };

            await _context.Books.AddAsync(book);
            _context.SaveChanges();

            var response = new
            {
                Message = "Book add successfully",
                AddBook = book,
                Status = "all ok"
            };

            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [Route("/book/update/{id}")]
        [HttpPut]
        public async Task<IActionResult> Modified(int id,[FromForm] BookViewModel b)
        {
            var book = await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
                return BadRequest($"id: {id} not valid !!");

            if (!ModelState.IsValid)
            {
                return BadRequest(b);
            }
          
             book = new book
            {
                title = b.title,
                categories = b.categories,
                authors = b.authors,
                description = b.description,
                published_year = b.published_year,
                average_rating = b.average_rating,
                num_pages = b.num_pages,
                url_image=b.url_image,
                ratings_count = b.ratings_count,

            };

            _context.SaveChanges();
            return Ok(book);
        }
        [Authorize(Roles = "Admin")]
        [Route("/book/delete/title/{title}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string title)
        {
            if(title == null)return BadRequest("enter a title of book!!");
            List<book> books =new List<book>(); 
            books = await _context.Books.FromSqlInterpolated($"SELECT * FROM [security].Books WHERE title = {title} ").ToListAsync();

            if (books.Count == 0)
                return BadRequest($"title of book : {title} not valid !!");

            foreach (book b in books)
                _context.Books.Remove(b);
            
            _context.SaveChanges();
            var responseData = new 
            {
                Message = "book deleted :(",
                Objects = books
            };
            return Ok(responseData);
        }
        [Authorize(Roles = "Admin")]
        [Route("/book/delete/id/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletebyId(int id)
        {
            var book = await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
                return BadRequest($"id: {id} not valid !!");

            _context.Books.Remove(book);
            _context.SaveChanges();
            var response = new
            {
                Message = "Book deleted successfully",
                DeletedBook = book,
                Status = "all ok"
            };

            return Ok(response);
        }
    }
}
