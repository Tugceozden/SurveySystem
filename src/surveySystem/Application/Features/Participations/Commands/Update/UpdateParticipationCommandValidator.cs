using FluentValidation;

namespace Application.Features.Participations.Commands.Update;

public class UpdateParticipationCommandValidator : AbstractValidator<UpdateParticipationCommand>
{
    public UpdateParticipationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PaticipantId).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
        RuleFor(c => c.Answers).NotEmpty();
    }
}