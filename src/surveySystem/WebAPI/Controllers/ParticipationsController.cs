using Application.Features.Participations.Commands.Create;
using Application.Features.Participations.Commands.Delete;
using Application.Features.Participations.Commands.Update;
using Application.Features.Participations.Queries.GetById;
using Application.Features.Participations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateParticipationCommand createParticipationCommand)
    {
        CreatedParticipationResponse response = await Mediator.Send(createParticipationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateParticipationCommand updateParticipationCommand)
    {
        UpdatedParticipationResponse response = await Mediator.Send(updateParticipationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedParticipationResponse response = await Mediator.Send(new DeleteParticipationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdParticipationResponse response = await Mediator.Send(new GetByIdParticipationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListParticipationQuery getListParticipationQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListParticipationListItemDto> response = await Mediator.Send(getListParticipationQuery);
        return Ok(response);
    }
}