using NArchitecture.Core.Application.Responses;

namespace Application.Features.SurveyResults.Queries.GetById;

public class GetByIdSurveyResultResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }
}