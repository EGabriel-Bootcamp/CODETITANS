using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Models;

namespace UserMgt.Application.Features.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<APIResponse>
    {
        public CreateUserDto CreateUser { get; set; }
    }
}
