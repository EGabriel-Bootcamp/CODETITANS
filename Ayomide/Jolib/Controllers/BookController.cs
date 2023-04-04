
using AutoMapper;
using Jolib.Dtos;
using Jolib.Entities;
using Jolib.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Jolib.Controllers
{
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _repo;
        public BookController(BookRepository repo)
        {
            _repo = repo;
         
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var res = await _repo.GetAllBooks();
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            if (id == 0)
                return BadRequest(id);
            var res = await _repo.GetBook(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpGet("GetAuthorsAttachedToBook")]
        public async Task<IActionResult> GetAuthorsAttachedToBook(int id)
        {
            if (id == 0)
                return BadRequest(id);
            var res = await _repo.GetAuthorsAttachedToBook(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody]BookDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var res = await _repo.CreateBook(model);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }


            return BadRequest(res);
        }
    }
}
