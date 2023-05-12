using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.AuthServices;

namespace Mauser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller 
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;  
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody]LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var res = await _service.LoginAsync(dto);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("CreateAdmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var res = await _service.CreateAsync(dto);
            if (res.Code.Equals("00"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
