using System.Net;
using Microsoft.AspNetCore.Mvc;
using TestCatalog.Host.Models.Requests;
using TestCatalog.Host.Services.Interfaces;

namespace TestCatalog.Host.Controllers;

[Route("question")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }


    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddQuestionAsync([FromBody] AddQuestionRequest question)
    {
        await _questionService.AddQuestionAsync(question);
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionRequest question)
    {
        await _questionService.UpdateQuestionAsync(question);
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteQuestionAsync([FromQuery] int id)
    {
        await _questionService.DeleteQuestionAsync(id);
        return Ok();
    }
}