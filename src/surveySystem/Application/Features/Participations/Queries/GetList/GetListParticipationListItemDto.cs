using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Participations.Queries.GetList;

public class GetListParticipationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid PaticipantId { get; set; }
    public Guid SurveyId { get; set; }
    public string? Answers { get; set; }
}