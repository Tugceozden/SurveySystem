using Application.Features.Participants.Commands.Create;
using Application.Features.Participants.Commands.Delete;
using Application.Features.Participants.Commands.Update;
using Application.Features.Participants.Queries.GetById;
using Application.Features.Participants.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateParticipantCommand createParticipantCommand)
    {
        CreatedParticipantResponse response = await Mediator.Send(createParticipantCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateParticipantCommand updateParticipantCommand)
    {
        UpdatedParticipantResponse response = await Mediator.Send(updateParticipantCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedParticipantResponse response = await Mediator.Send(new DeleteParticipantCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdParticipantResponse response = await Mediator.Send(new GetByIdParticipantQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListParticipantQuery getListParticipantQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListParticipantListItemDto> response = await Mediator.Send(getListParticipantQuery);
        return Ok(response);
    }
}