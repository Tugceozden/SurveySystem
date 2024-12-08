using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participations.Commands.Delete;

public class DeletedParticipationResponse : IResponse
{
    public Guid Id { get; set; }
}