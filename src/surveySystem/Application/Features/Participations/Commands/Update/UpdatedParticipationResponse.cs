using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participations.Commands.Update;

public class UpdatedParticipationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PaticipantId { get; set; }
    public Guid SurveyId { get; set; }
    public string? Answers { get; set; }
}