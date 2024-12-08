using NArchitecture.Core.Application.Dtos;

namespace Application.Features.SurveyResults.Queries.GetList;

public class GetListSurveyResultListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }
}