using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using UserApp.Domain.Entities;
using UserApp.Services.Service.Contract;

namespace UserApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDistributedCache _distributedCache;
        public UsersController(IUserService userService, IDistributedCache distributedCache)
        {
            _userService = userService;
            _distributedCache = distributedCache;

        }


        [HttpGet("GetAllUsers")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "usersList";
            string serializedUserList;
            var redisUserList = await _distributedCache.GetAsync(cacheKey);
            var users = await _userService.GetAllUsers();
            if (redisUserList != null)
            {
                serializedUserList = Encoding.UTF8.GetString(redisUserList);
                users = JsonConvert.DeserializeObject<List<User>>(serializedUserList);
            }
            else
            {
                users = await _userService.GetAllUsers();
                serializedUserList = JsonConvert.SerializeObject(users);
                redisUserList = Encoding.UTF8.GetBytes(serializedUserList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisUserList, options);
            }

            //var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("GetUserById")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<User>> GetUserId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(userId);
            }
            var user = await _userService.GetSingleUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }


        [HttpPost("CreateNewUser")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser([FromBody] User createUser)
        {
            var cacheKey = "newUser";
            if (createUser == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var newUser = await _userService.CreateUser(createUser);

            await _distributedCache.RemoveAsync(cacheKey);
            //Return new company created
            return Ok(newUser);
        }


        [HttpDelete("DeleteUser")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> DeleteUser([FromQuery] int userId)
        {
            var cacheKey = "deleteUSer";
            if (userId == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var deletedUser = await _userService.DeleteUser(userId);

            await _distributedCache.RemoveAsync(cacheKey);



            //Return new company created
            return NoContent();
        }

        [HttpPut("UpdateUser")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not found
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User entity)
        {
            var cacheKey = "update";
            if (entity == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var updatedUser = await _userService.UpdateUser(entity);

            await _distributedCache.RemoveAsync(cacheKey);
            //Return new company created
            return Ok(updatedUser);
        }

        [HttpGet("SearchUser")]
       // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<User>>> SearchUser(string searchTerm)
        {
            var cacheKey = "usersSearch";
            string serializedUserList;
            var redisUserList = await _distributedCache.GetAsync(cacheKey);
            var users = await _userService.SearchUser(searchTerm);
            if (redisUserList != null)
            {
                serializedUserList = Encoding.UTF8.GetString(redisUserList);
                users = JsonConvert.DeserializeObject<List<User>>(serializedUserList);
            }
            else
            {
                users = await _userService.SearchUser(searchTerm);
                serializedUserList = JsonConvert.SerializeObject(users);
                redisUserList = Encoding.UTF8.GetBytes(serializedUserList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisUserList, options);
            }
           // var users = await _userService.SearchUser(searchTerm);
            return Ok(users);
        }
    }
}