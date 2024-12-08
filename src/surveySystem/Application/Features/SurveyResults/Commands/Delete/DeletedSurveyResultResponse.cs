using NArchitecture.Core.Application.Responses;

namespace Application.Features.SurveyResults.Commands.Delete;

public class DeletedSurveyResultResponse : IResponse
{
    public Guid Id { get; set; }
}