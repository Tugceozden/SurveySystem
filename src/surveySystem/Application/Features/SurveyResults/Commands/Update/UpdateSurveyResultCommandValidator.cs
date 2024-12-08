using FluentValidation;

namespace Application.Features.SurveyResults.Commands.Update;

public class UpdateSurveyResultCommandValidator : AbstractValidator<UpdateSurveyResultCommand>
{
    public UpdateSurveyResultCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.ParticipantId).NotEmpty();
    }
}