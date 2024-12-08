using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participations.Queries.GetById;

public class GetByIdParticipationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PaticipantId { get; set; }
    public Guid SurveyId { get; set; }
    public string? Answers { get; set; }
}