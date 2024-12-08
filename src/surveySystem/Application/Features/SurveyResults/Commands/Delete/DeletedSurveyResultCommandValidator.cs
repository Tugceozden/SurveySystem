using FluentValidation;

namespace Application.Features.SurveyResults.Commands.Delete;

public class DeleteSurveyResultCommandValidator : AbstractValidator<DeleteSurveyResultCommand>
{
    public DeleteSurveyResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}