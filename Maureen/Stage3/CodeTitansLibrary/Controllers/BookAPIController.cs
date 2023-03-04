using CodeTitansLibrary.DataAccess;
using CodeTitansLibrary.Entities;
using CodeTitansLibrary.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTitansLibrary.Controllers
{
    [ApiController]
    [Route("api/BookAPIController")]
    public class BookAPIController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public BookAPIController(LibraryDbContext db)
        {
            _db= db;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateBook(BookDTO bookDto)
        {
            Book book = new();
            if (bookDto == null)
            {
                return BadRequest(bookDto);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(bookDto);
            }

            book.AuthorId = bookDto.AuthorId;
            book.ISBN = bookDto.ISBN;
            book.BookTitle = bookDto.BookTitle;
            book.Price = bookDto.Price;

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetABook", book.Id, book);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            IEnumerable<Book> AllBooks = await _db.Books.ToListAsync();
            return Ok(AllBooks);
        }

        [HttpGet("id", Name = "GetABook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> GetABook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }




    }
}
