using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgt.Application.Contracts.Identity;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Models.Identity;

namespace UserMgt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register(RegistrationRequest request)
        {
            
            return Ok(await _authService.Register(request));
        }
    }
}
