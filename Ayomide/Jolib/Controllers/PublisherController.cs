using AutoMapper;
using Jolib.Data;
using Jolib.Dtos;
using Jolib.Entities;
using Jolib.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Jolib.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherRepository _repo;
        private readonly IMapper _mapper;
        public PublisherController(PublisherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("GetAllPublishers")]
        public IActionResult GetAllPublishers()
        {
            var res =  _repo.GetAllPublisher();
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("GetPublisher")]
        public IActionResult GetPublisher(int id)
        {
            if(id == 0)
               return BadRequest(id);
            var res =  _repo.GetPublisher(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpGet("GetAuthorsAttachedToAPublisher")]
        public IActionResult GetAuthorsAttachedToAPublisher(int id)
        {
            if (id == 0)
                return BadRequest(id);
            var res =  _repo.GetAuthorsAttachedToAPublisher(id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
        [HttpPost("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher([FromBody]PublisherDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = _mapper.Map<Publisher>(model);
            var res = await _repo.CreatePublisher(publisher);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }


            return BadRequest(res);
        }
    }
}
