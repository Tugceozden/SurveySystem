using FluentValidation;

namespace Application.Features.Questions.Commands.Update;

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.QuestionText).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Options).NotEmpty();
        RuleFor(c => c.Answer).NotEmpty();
    }
}