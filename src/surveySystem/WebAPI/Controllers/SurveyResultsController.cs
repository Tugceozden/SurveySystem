using Application.Features.SurveyResults.Commands.Create;
using Application.Features.SurveyResults.Commands.Delete;
using Application.Features.SurveyResults.Commands.Update;
using Application.Features.SurveyResults.Queries.GetById;
using Application.Features.SurveyResults.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SurveyResultsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSurveyResultCommand createSurveyResultCommand)
    {
        CreatedSurveyResultResponse response = await Mediator.Send(createSurveyResultCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSurveyResultCommand updateSurveyResultCommand)
    {
        UpdatedSurveyResultResponse response = await Mediator.Send(updateSurveyResultCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSurveyResultResponse response = await Mediator.Send(new DeleteSurveyResultCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSurveyResultResponse response = await Mediator.Send(new GetByIdSurveyResultQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSurveyResultQuery getListSurveyResultQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSurveyResultListItemDto> response = await Mediator.Send(getListSurveyResultQuery);
        return Ok(response);
    }
}