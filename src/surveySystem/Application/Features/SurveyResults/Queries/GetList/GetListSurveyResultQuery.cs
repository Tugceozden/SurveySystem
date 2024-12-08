using Application.Features.SurveyResults.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.SurveyResults.Constants.SurveyResultsOperationClaims;

namespace Application.Features.SurveyResults.Queries.GetList;

public class GetListSurveyResultQuery : IRequest<GetListResponse<GetListSurveyResultListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSurveyResults({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSurveyResults";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSurveyResultQueryHandler : IRequestHandler<GetListSurveyResultQuery, GetListResponse<GetListSurveyResultListItemDto>>
    {
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly IMapper _mapper;

        public GetListSurveyResultQueryHandler(ISurveyResultRepository surveyResultRepository, IMapper mapper)
        {
            _surveyResultRepository = surveyResultRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSurveyResultListItemDto>> Handle(GetListSurveyResultQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SurveyResult> surveyResults = await _surveyResultRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSurveyResultListItemDto> response = _mapper.Map<GetListResponse<GetListSurveyResultListItemDto>>(surveyResults);
            return response;
        }
    }
}