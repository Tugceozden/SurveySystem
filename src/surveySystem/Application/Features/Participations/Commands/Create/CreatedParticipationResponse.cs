using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participations.Commands.Create;

public class CreatedParticipationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PaticipantId { get; set; }
    public Guid SurveyId { get; set; }
    public string? Answers { get; set; }
}