using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.DTOs;
using UserApp.Domain.Entities;
using UserApp.Services.Service.Contract;

namespace UserApp.Services.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
         _configuration = configuration;
        }

        public async Task<AppUser> CreateAsync(UserRegistrationDto model)
        {
            if (model is null) throw new ArgumentNullException(message: "Invalid Details Provided", null);

            AppUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                if (!await _roleManager.RoleExistsAsync(UserRoles.SystemAdministrator))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.SystemAdministrator));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, UserRoles.User);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(message: "Account creation not succeeded");
                }
            }

            return user;
        }
        public async Task<JwtSecurityToken> LoginAsync(LoginModelDto model)
        {
            try
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = GetToken(authClaims);

                    return token;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
