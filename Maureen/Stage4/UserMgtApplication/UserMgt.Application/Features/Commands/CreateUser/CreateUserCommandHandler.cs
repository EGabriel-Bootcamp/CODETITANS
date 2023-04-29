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
using UserMgt.Domain.Constants;

namespace UserMgt.Application.Features.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, APIResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public APIResponse response;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            response = new();
        }
        public async Task<APIResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = _mapper.Map<User>(request.CreateUser);

            if (userToCreate == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            if (userToCreate.Gender != Gender.Male && userToCreate.Gender != Gender.Female)
            {
                response = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new() { "Invalid Gender" },
                    IsSuccess = false
                };
                return response;
            }

            await _userRepository.CreateAsync(userToCreate);

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = userToCreate;
            return response;
        }
    }
}
