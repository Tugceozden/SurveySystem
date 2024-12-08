using NArchitecture.Core.Application.Responses;

namespace Application.Features.Surveys.Commands.Create;

public class CreatedSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid ParticipantId { get; set; }
}