using AutoMapper;
using CA_Application.Repository;
using CA_Application.Services;
using CA_Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CA_Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
			_userService = userService;
			_mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
		{
			IEnumerable<User> users = _userService.GetAll();
			var usersList = _mapper.Map<User>(users);
			//var usersList = _mapper.Map<UserDTO>(users);


			//return Ok(_mapper.Map<UserDTO>(users));
			return Ok(usersList);
		}


		// GET api/<UsersController>/5
		[HttpGet("{id}", Name = "GetOneById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<UserDTO> GetOneById(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			
			var user = _userService.GetOne(x => x.Id == id);

			if (user == null)
			{
				return NotFound();
			}
			
			return Ok(user);
		}

		// POST api/<UsersController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult CreateUser([FromBody] UserCreateDTO userCreateDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(userCreateDTO);
			}

			var model = _mapper.Map<User>(userCreateDTO);
			
			_userService.Add(model);
			_userService.Save();

			//return CreatedAtRoute("GetOneById", model.Id, model);
			return Ok(model);
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public ActionResult<UserDTO> UpdateUser(int id, [FromBody] UserUpdateDTO userUpdateDTO)
		{
			if (id == 0 )
			{
				return BadRequest();
			}

			var userFromDb = _userService.GetOne(x => x.Id == id);

			//userFromDb = _mapper.Map<User>(userUpdateDTO);

			userFromDb.Address = userUpdateDTO.Address;
			userFromDb.City = userUpdateDTO.City;
			userFromDb.MarritalStatus = userUpdateDTO.MarritalStatus;





			_userService.Update(userFromDb);
			_userService.Save();
			return Ok(userFromDb);
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult Delete(int id)
		{
			var user = _userService.GetOne(x => x.Id == id);
			_userService.Delete(user);
			_userService.Save();
			return Ok();
		}
	}
}
