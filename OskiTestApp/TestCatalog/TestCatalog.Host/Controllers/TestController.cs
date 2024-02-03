﻿using Microsoft.AspNetCore.Http;
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
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TestDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTestAsync([FromBody] int testId)
        {
            var result = await _testService.GetTestAsync(testId);
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