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

namespace UserMgt.Application.Features.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, APIResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public APIResponse response;

        public DeleteUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<APIResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                response = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new() { "No id was entered" },
                    IsSuccess = false
                };
                return response;
            }
            var user = await _userRepository.GetOneAsync(x => x.Id == request.Id);

            if (user == null)
            {
                response = new()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessages = new() { "User does not exist" },
                    IsSuccess = false
                };
                return response;
            }
            
            await _userRepository.DeleteAsync(user);
            
            response = new()
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessages = new() { "Successfully deleted user" },
                IsSuccess = true
            };
            return response;
        }
    }
}
