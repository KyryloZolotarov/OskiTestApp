using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserProfiles.Host.Models.Dtos;
using UserProfiles.Host.Models.Requests;
using UserProfiles.Host.Services.Interfaces;

namespace UserProfiles.Host.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("new")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
        {
            var result = await _userService.AddUserAsync(user);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest user)
        {
            await _userService.UpdateUserAsync(user);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAsync([FromBody] string id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest login)
        {
            var user = await _userService.LoginAsynnc(login);
            return Ok(user);
        }
    }
}
