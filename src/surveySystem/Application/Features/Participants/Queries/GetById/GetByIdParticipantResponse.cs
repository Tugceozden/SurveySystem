using NArchitecture.Core.Application.Responses;

namespace Application.Features.Participants.Queries.GetById;

public class GetByIdParticipantResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public Guid UserID { get; set; }
}