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
using UserMgt.Domain;

namespace UserMgt.Application.Features.Queries.GetOneUser
{
    public class GetOneUserQueryHandler : IRequestHandler<GetOneUserQuery, APIResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public APIResponse response;

        public GetOneUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<APIResponse> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                response = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                };
                return response;
            }

            var user = await _userRepository.GetOneAsync(x => x.Id == request.Id);
            
            if (user == null)
            {
                response = new()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new() { "User does not exist" }
                };
                return response;
            }

            var userDto = _mapper.Map<User>(user);

            response = new()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = userDto
            };
            return response;
        }
    }
}
