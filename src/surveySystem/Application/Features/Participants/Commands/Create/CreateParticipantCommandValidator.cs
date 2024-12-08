using FluentValidation;

namespace Application.Features.Participants.Commands.Create;

public class CreateParticipantCommandValidator : AbstractValidator<CreateParticipantCommand>
{
    public CreateParticipantCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Surname).NotEmpty();
        RuleFor(c => c.Age).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.UserID).NotEmpty();
    }
}