using NArchitecture.Core.Application.Responses;

namespace Application.Features.SurveyResults.Commands.Create;

public class CreatedSurveyResultResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }
}