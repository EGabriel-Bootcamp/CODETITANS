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

namespace UserMgt.Application.Features.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, APIResponse>
    {
        private readonly IUserRepository _userRepository;
        public IMapper _mapper;
        public APIResponse response;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            response = new();
        }

        public async Task<APIResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = usersDto;

            return response;
        }
    }
}
