using FluentValidation;

namespace Application.Features.SurveyResults.Commands.Create;

public class CreateSurveyResultCommandValidator : AbstractValidator<CreateSurveyResultCommand>
{
    public CreateSurveyResultCommandValidator()
    {
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.ParticipantId).NotEmpty();
    }
}