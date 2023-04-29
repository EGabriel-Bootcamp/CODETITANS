using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Models;

namespace UserMgt.Application.Features.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<APIResponse>
    {
    }
}
