using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserMgt.Application.Features.Commands.CreateUser;
using UserMgt.Application.Features.Commands.DeleteUser;
using UserMgt.Application.Features.Commands.EditUser;
using UserMgt.Application.Features.Models;
using UserMgt.Application.Features.Queries.FilterUsers;
using UserMgt.Application.Features.Queries.GetAllUsers;
using UserMgt.Application.Features.Queries.GetOneUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMgt.API.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            _response = await _mediator.Send(new GetAllUsersQuery());
            
            return Ok(_response);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            var command = new GetOneUserQuery();
            command.Id = id;
            _response = await _mediator.Send(command);
            return _response;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post(CreateUserCommand createUser)
        {
            _response = await _mediator.Send(createUser);
            return (_response);
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Put(EditUserCommand editUsercommand)
        {
            _response = await _mediator.Send(editUsercommand);
            return _response;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var command = new DeleteUserCommand();
            command.Id = id;
            _response = await _mediator.Send(command);
            return _response;
        }

        [HttpGet("searchUser")]
        public async Task<ActionResult<APIResponse>> Filter(string searchText)
        {
            var command = new FilterUsersQuery();
            command.SearchText = searchText;

            _response = await _mediator.Send(command);
            return _response;
        }
    }
}
