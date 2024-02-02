using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCatalog.Host.Models.Dtos;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Services;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testManageService;
        public TestController(ITestService testManageService)
        {
            _testManageService = testManageService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TestDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTestAsync([FromBody] int testId)
        {
            var result = await _testManageService.GetTestAsync(testId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddTestAsync([FromBody] AddTestRequest test)
        {
            await _testManageService.AddTestAsync(test);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateTestAsync([FromBody] UpdateTestRequest test)
        {
            await _testManageService.UpdateTestAsync(test);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteTestAsync([FromQuery] int id)
        {
            await _testManageService.DeleteTestAsync(id);
            return Ok();
        }
    }
}
