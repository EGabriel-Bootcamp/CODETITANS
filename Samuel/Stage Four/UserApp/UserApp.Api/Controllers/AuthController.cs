using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserApp.Domain.DTOs;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Services.Service.Contract;

namespace UserApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;

        public AuthController(ILogger<AuthController> logger, IAccountService accountService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Model cannot be null");

            try
            {
                var user = await _accountService.CreateAsync(model);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Account creation failed");
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginModelDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid login model");

            try
            {
                var token = await _accountService.LoginAsync(model);

                if (token == null) return Unauthorized();

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception e)
            {
                return BadRequest("Error occurred");
            }
        }
    }
}
