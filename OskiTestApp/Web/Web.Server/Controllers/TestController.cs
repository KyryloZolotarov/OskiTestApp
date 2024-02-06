using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly ITestService _testService;

    public TestController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet("getAvailableTests")]
    [ProducesResponseType(typeof(TestsNamesViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAvailableTests()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
        var result = await _testService.GetAvailableTests(userId);
        return Ok(result);
    }

    [HttpGet("getTest")]
    [ProducesResponseType(typeof(TestViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetSelectedTest([FromQuery] int testId)
    {
        var result = await _testService.GetSelectedTest(testId);
        return Ok(result);
    }

    [HttpGet("getPassedTests")]
    [ProducesResponseType(typeof(IEnumerable<PassedTestViewModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetPassedTests([FromQuery] string userId)
    {
        var result = await _testService.GetPassedTests(userId);
        return Ok(result);
    }
}