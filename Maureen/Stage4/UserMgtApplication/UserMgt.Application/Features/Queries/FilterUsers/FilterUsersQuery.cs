using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Features.Models;

namespace UserMgt.Application.Features.Queries.FilterUsers
{
    public class FilterUsersQuery : IRequest<APIResponse>
    {
        public string SearchText { get; set; }
    }
}
