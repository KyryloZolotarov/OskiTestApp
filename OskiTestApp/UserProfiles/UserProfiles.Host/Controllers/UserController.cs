using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserProfiles.Host.Models.Requests;
using UserProfiles.Host.Services.Interfaces;

namespace UserProfiles.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserRequest user)
        {
            await _userService.AddUserAsync(user);
            return Ok();
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
    }
}
