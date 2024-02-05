using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("getavailabletests")]
        [ProducesResponseType(typeof(TestsNamesViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvailableTests([FromQuery] string userId)
        {
            var result = await _testService.GetAvailableTests(userId);
            return Ok(result);
        }

        [HttpGet("gettest")]
        [ProducesResponseType(typeof(TestViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSelectedTest([FromQuery] int testId)
        {
            var result = await _testService.GetSelectedTest(testId);
            return Ok(result);
        }

        [HttpGet("getpassedtests")]
        [ProducesResponseType(typeof(IEnumerable<PassedTestViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPassedTests([FromQuery] string userId)
        {
            var result = await _testService.GetPassedTests(userId);
            return Ok(result);
        }
    }
}
