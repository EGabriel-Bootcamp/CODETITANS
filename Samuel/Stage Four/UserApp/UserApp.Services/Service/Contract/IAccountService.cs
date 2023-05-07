using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.DTOs;
using UserApp.Domain.Entities;

namespace UserApp.Services.Service.Contract
{
    public interface IAccountService 
    {
        Task<AppUser> CreateAsync(UserRegistrationDto model);
        Task<JwtSecurityToken> LoginAsync(LoginModelDto model);
    }
}
