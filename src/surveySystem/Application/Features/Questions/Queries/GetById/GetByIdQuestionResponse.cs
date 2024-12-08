using NArchitecture.Core.Application.Responses;

namespace Application.Features.Questions.Queries.GetById;

public class GetByIdQuestionResponse : IResponse
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public Guid SurveyId { get; set; }
    public int Order { get; set; }
    public string Options { get; set; }
    public string? Answer { get; set; }
}