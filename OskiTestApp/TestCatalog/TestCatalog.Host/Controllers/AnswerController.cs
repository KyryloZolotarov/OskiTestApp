using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Services;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Controllers
{
    [Route("answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAnswerAsync([FromBody] AddAnswerRequest answer)
        {
            await _answerService.AddAnswerAsync(answer);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAnswerAsync([FromBody] UpdateAnswerRequest answer)
        {
            await _answerService.UpdateAnswerAsync(answer);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAnswerAsync([FromQuery] int id)
        {
            await _answerService.DeleteAnswerAsync(id);
            return Ok();
        }
    }
}
