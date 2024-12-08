using NArchitecture.Core.Application.Responses;

namespace Application.Features.SurveyResults.Commands.Update;

public class UpdatedSurveyResultResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }
}