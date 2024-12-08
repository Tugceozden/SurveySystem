using FluentValidation;

namespace Application.Features.Participants.Commands.Update;

public class UpdateParticipantCommandValidator : AbstractValidator<UpdateParticipantCommand>
{
    public UpdateParticipantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Surname).NotEmpty();
        RuleFor(c => c.Age).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.UserID).NotEmpty();
    }
}