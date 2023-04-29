using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Models;

namespace UserMgt.Application.Features.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<APIResponse>
    {
        public int Id { get; set; }
    }
}
