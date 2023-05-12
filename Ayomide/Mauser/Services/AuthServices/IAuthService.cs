using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse> CreateAsync(CreateAdminDto dto);
        Task<ApiResponse> LoginAsync(LoginDto dto);
    }
}
