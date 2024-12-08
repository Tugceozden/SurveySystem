using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participants.Commands.Delete;

public class DeletedParticipantResponse : IResponse
{
    public Guid Id { get; set; }
}