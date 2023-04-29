using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Persistence;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Features.Queries.GetAllUsers;

namespace UserMgt.Application.Features.Queries.FilterUsers
{
    public class FilterUsersQueryHandler : IRequestHandler<FilterUsersQuery, APIResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public FilterUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<APIResponse> Handle(FilterUsersQuery request, CancellationToken cancellationToken)
        {
            var filtered = await _userRepository.UserFilter(request.SearchText);
            IEnumerable<UserDto> filteredDto = _mapper.Map<IEnumerable<UserDto>>(filtered);

            APIResponse response = new()
            {
                IsSuccess = true,
                Result = filteredDto,
                StatusCode = HttpStatusCode.OK,
            };
            return response;
        }
    }
}
