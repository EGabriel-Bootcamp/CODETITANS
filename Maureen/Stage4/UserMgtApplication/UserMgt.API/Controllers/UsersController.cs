using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using UserMgt.Application.Features.Commands.CreateUser;
using UserMgt.Application.Features.Commands.DeleteUser;
using UserMgt.Application.Features.Commands.EditUser;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Features.Queries.GetAllUsers;
using UserMgt.Application.Features.Queries.GetOneUser;
using UserMgt.Application.Models.Identity;


namespace UserMgt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public APIResponse _response;

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
            _response = new();
        }


        // GET: api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetUsers([FromQuery] string? searcText)
        {
            _response = await _mediator.Send(new GetAllUsersQuery(searcText));

            if (!User.IsInRole(UserRoles.General) || !User.IsInRole(UserRoles.Admin))
                return Unauthorized(_response);

            return Ok(_response);
        }

        // GET api/<ValuesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            var command = new GetOneUserQuery();
            command.Id = id;
            _response = await _mediator.Send(command);


            return _response;
        }

        // POST api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<APIResponse>> Post(CreateUserCommand createUser)
        {
            if (!User.IsInRole(UserRoles.Admin))
                return Unauthorized();

            _response = await _mediator.Send(createUser);
            return (_response);
        }

        // PUT api/<ValuesController>/5

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Put(EditUserCommand editUsercommand)
        {
            _response = await _mediator.Send(editUsercommand);
            return _response;
        }

        // DELETE api/<ValuesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var command = new DeleteUserCommand();
            command.Id = id;
            _response = await _mediator.Send(command);
            return _response;
        }

    }
}
