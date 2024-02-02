using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserTest.Host.Models.Requests;
using UserTest.Host.Services.Interfaces;

namespace UserTest.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestController : ControllerBase
    {
        private readonly IUserTestService _userTestManageService;
        public UserTestController(IUserTestService userTestManageService)
        {
            _userTestManageService = userTestManageService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserTestRequest userTest)
        {
            await _userTestManageService.AddUserTestAsync(userTest);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserTestAsync([FromBody] UpdateUserTestRequest userTest)
        {
            await _userTestManageService.UpdateUserTestAsync(userTest);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserTestAsync([FromQuery] int id)
        {
            await _userTestManageService.DeleteUserTestAsync(id);
            return Ok();
        }
    }
}
