using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Models.Identity;

namespace UserMgt.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<APIResponse> Login(AuthRequest request);
        Task<APIResponse> Register(RegistrationRequest request);
    }
}
