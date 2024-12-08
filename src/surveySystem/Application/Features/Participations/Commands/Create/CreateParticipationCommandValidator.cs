using FluentValidation;

namespace Application.Features.Participations.Commands.Create;

public class CreateParticipationCommandValidator : AbstractValidator<CreateParticipationCommand>
{
    public CreateParticipationCommandValidator()
    {
        RuleFor(c => c.PaticipantId).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.Answers).NotEmpty();
    }
}