using FluentValidation;

namespace Application.Features.Participants.Commands.Delete;

public class DeleteParticipantCommandValidator : AbstractValidator<DeleteParticipantCommand>
{
    public DeleteParticipantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}