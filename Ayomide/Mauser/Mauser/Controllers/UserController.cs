using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.UserServices;

namespace Mauser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _service= service;
            _mapper= mapper;    
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var res = await _service.GetUsers();
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser(int Id)
        {
            if (Id <=  0)
                return BadRequest(Id);
            var res = await _service.GetUser(Id);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody]UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var res = await _service.CreateUser(user);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("EditUser")]
        public async Task<ActionResult> UpdateUser([FromBody]UserDto dto, int userId)
        {
            if(userId <= 0 || !ModelState.IsValid)
                return BadRequest();
            var user = _mapper.Map<User>(dto);
            user.Id= userId;
            var res = await _service.UpdateUser(user);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromBody]List<int> userIds)
        {
            if (!userIds.Any() || !ModelState.IsValid)
                return BadRequest();
                var res = await _service.DeleteUsers(userIds);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
