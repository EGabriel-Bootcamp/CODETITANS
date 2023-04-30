using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Identity;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Models.Identity;
using UserMgt.Identity.Models;

namespace UserMgt.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        public APIResponse jwtResponse { get; set; }

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._jwtSettings = jwtSettings.Value;
        }

        public async Task<APIResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
            {
                jwtResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new() { $"{request.Email} not found" }
                };
                return jwtResponse;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded == false)
            {
                jwtResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new() { "Invalid Login"}
                };

                return jwtResponse;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateTokenAsync(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            jwtResponse = new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Result = response
            };
            return jwtResponse;
        }


        public async Task<APIResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Username,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.General);

                jwtResponse = new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = new RegistrationResponse() { UserId = user.Id },
                };

                return jwtResponse;
            }
            else
            {
                jwtResponse = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                };
                foreach (var err in result.Errors)
                {
                    jwtResponse.ErrorMessages.Add($"{err.Description}\n");
                }

                return jwtResponse;
            }

        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
        {
            //Get User Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            //Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            
            // Convert roles to list of Claims
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();


            //merge all claims together

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName ?? "TestUSername"),

                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "admin@localhost.com"),
                new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                new Claim("uid", user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signInCredentials = new SigningCredentials(
                symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signInCredentials
                );

            return jwtSecurityToken;
        
        }
    }
}
