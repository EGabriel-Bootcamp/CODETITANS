using CodeTitansLibrary.DataAccess;
using CodeTitansLibrary.Entities;
using CodeTitansLibrary.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeTitansLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherAPIController : ControllerBase
    {
        private readonly LibraryDbContext _db;
        public PublisherAPIController(LibraryDbContext db)
        {
            _db = db;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePublisher(PublisherDTO publisherDto)
        {
            Publisher publisher = new();
            if (publisherDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            publisher.PublisherName = publisherDto.PublisherName;


            await _db.Publishers.AddAsync(publisher);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetAPublisher", publisher.Id, publisher);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers() 
        {
            var publishers = await _db.Publishers.ToListAsync();
            if (publishers == null)
            {
                return NoContent();
            }
            return Ok(publishers);
        }

        
        
        
        [HttpGet("id", Name = "GetAPublisher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Publisher>> GetAPublisher(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var publisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsAttachedToAPublisher(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var publisher = await _db.Publishers.FirstOrDefaultAsync(x => x.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }
            var PublisherAuthors = _db.Authors.Where(x => x.PublisherId == publisher.Id);
            return Ok(PublisherAuthors);
        }
    }
}
