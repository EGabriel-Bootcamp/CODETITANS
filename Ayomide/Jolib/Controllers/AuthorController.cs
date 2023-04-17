using AutoMapper;
using Jolib.Dtos;
using Jolib.Entities;
using Jolib.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Jolib.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorRepository _repo;
        public AuthorController(AuthorRepository repo)
        {
            _repo= repo;
          
        }
        [HttpGet("GetAllAuthors")]
        public IActionResult GetAllAuthors()
        {
            var res =    _repo.GetAllAuthors();
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("GetAuthor")]
        public IActionResult GetAuthor(int id)
        {
            if (id == 0)
                return BadRequest(id);
            var res = _repo.GetAuthor(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpGet("GetBooksAttachedToAuthor")]
        public IActionResult GetBooksAttachedToAuthor(int id)
        {
            if (id == 0)
                return BadRequest(id);
            var res = _repo.GetBooksAttachedToAuthor(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _repo.CreateAuthor(model);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }


            return BadRequest(res);
        }
    }
}
