using FluentValidation;

namespace Application.Features.Questions.Commands.Create;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(c => c.QuestionText).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Options).NotEmpty();
        RuleFor(c => c.Answer).NotEmpty();
    }
}