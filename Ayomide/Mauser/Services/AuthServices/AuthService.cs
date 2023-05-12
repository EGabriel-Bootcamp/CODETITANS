using Domain.Dtos;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> CreateAsync(CreateAdminDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if(userExists != null)
                return new ApiResponse() { Code = "25", Description = "User exists with email " + dto.Email, Data = null };
            
            var admin = new AppUser()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                UserName = dto.UserName,
                EmailConfirmed = true,
            };
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            var result = await _userManager.CreateAsync(admin, dto.Password);
            if(!result.Succeeded)
                return new ApiResponse() { Code = "25", Description = "An Error occurred ceating accounts", Data = null };

            await _userManager.AddToRoleAsync(admin, UserRoles.Admin);

            return new ApiResponse() { Code = "00", Description = "Account creation successful", Data = null };


        }

        public async Task<ApiResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null )
                return new ApiResponse() { Code = "25", Description = "Email or password is not correct", Data = null };

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if(!result)
                return new ApiResponse() { Code = "25", Description = "Email or password is not correct", Data = null };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                         new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var fullToken =  GetToken(authClaims);
           var token = new JwtSecurityTokenHandler().WriteToken(fullToken);
            
                    
            return new ApiResponse() { Code = "00", Description = "Success", Data = token};

        }

        
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:ValidIssuer"],
                audience: _configuration["JwtConfig:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
