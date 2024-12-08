using FluentValidation;

namespace Application.Features.Participations.Commands.Delete;

public class DeleteParticipationCommandValidator : AbstractValidator<DeleteParticipationCommand>
{
    public DeleteParticipationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}