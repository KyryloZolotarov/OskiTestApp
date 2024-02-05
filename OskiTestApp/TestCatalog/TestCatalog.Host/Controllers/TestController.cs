using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Models.Responses;
using TestCatalog.Host.Services;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("gettest")]
        [ProducesResponseType(typeof(TestResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTestAsync([FromRoute] int testId)
        {
            var result = await _testService.GetTestAsync(testId);
            return Ok(result);
        }

        [HttpPost("gettestsnames")]
        [ProducesResponseType(typeof(TestsNamesResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTestsNamesAsync([FromBody] TestsNamesRequest testsIds)
        {
            var result = await _testService.GetTestsNamesAsync(testsIds);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddTestAsync([FromBody] AddTestRequest test)
        {
            await _testService.AddTestAsync(test);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTestAsync([FromBody] UpdateTestRequest test)
        {
            await _testService.UpdateTestAsync(test);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteTestAsync([FromQuery] int id)
        {
            await _testService.DeleteTestAsync(id);
            return Ok();
        }
    }
}
