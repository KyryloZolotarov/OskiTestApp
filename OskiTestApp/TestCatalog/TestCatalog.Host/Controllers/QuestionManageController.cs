using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionManageController : ControllerBase
    {
        private readonly IQuestionManageService _questionManageService;
        public QuestionManageController(IQuestionManageService questionManageService)
        {
            _questionManageService = questionManageService;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddQuestionAsync([FromBody] AddQuestionRequest question)
        {
            await _questionManageService.AddQuestionAsync(question);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionRequest question)
        {
            await _questionManageService.UpdateQuestionAsync(question);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteQuestionAsync([FromQuery] int id)
        {
            await _questionManageService.DeleteQuestionAsync(id);
            return Ok();
        }
    }
}
