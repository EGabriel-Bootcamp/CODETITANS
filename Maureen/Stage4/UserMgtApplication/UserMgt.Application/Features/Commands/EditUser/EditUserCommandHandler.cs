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

namespace UserMgt.Application.Features.Commands.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, APIResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public APIResponse response;

        public EditUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<APIResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {

            if (request.EditUserDto == null)
            {
                response = new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = new() { "No edit was made" },
                    IsSuccess = false
                };
                return response;
            }

            var user = _mapper.Map<User>(request.EditUserDto);

            await _userRepository.UpdateAsync(user);

            var userFromDb = _userRepository.GetOneAsync(x => x.Id == user.Id);
            
            response = new()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = userFromDb,
            };
            return response;
        }
    }
}
