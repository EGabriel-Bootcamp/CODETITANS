using CodeTitansLibrary.DataAccess;
using CodeTitansLibrary.Entities;
using CodeTitansLibrary.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTitansLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorAPIController : ControllerBase
    {
        private readonly LibraryDbContext _db;

        public AuthorAPIController(LibraryDbContext db)
        {
            _db = db;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAuthor(AuthorDTO authorDto)
        {
            Author author = new();
            if (authorDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_db.Publishers.Any(x => x.Id == authorDto.PublisherId))
            {
                return BadRequest();
            }

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.PublisherId  = authorDto.PublisherId;
            author.Biography = authorDto.Biography;


            await _db.Authors.AddAsync(author);
            await _db.SaveChangesAsync();
            
            return CreatedAtRoute("GetAnAuthor", author.Id, author);
        }

        [HttpGet("id", Name = "GetAnAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Author>> GetAnAuthor(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authorsList = await _db.Authors.Include(x => x.Publisher).ToListAsync();
            if (authorsList == null)
            {
                return NoContent();
            }
            return Ok(authorsList);
        }


        //[HttpGet("id")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAttachedToAnAuthor(int id)
        //{

        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var author = await _db.Authors.FirstOrDefaultAsync(x => x.Id == id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }

        //    IEnumerable<Book> books = _db.Books.Where(x => x.AuthorId == id)
        //                                        .Include(x => x.Author).ToList();
        //    return Ok(books);
        //}


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Book>> GetAllBooksAttachedToAnAuthor(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            var author = _db.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return _db.Books.Where(x => x.AuthorId == id).ToList();
        }
    }
}
