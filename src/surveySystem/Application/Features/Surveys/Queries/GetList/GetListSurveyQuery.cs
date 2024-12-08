using Application.Features.Surveys.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Surveys.Constants.SurveysOperationClaims;

namespace Application.Features.Surveys.Queries.GetList;

public class GetListSurveyQuery : IRequest<GetListResponse<GetListSurveyListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSurveys({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSurveys";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSurveyQueryHandler : IRequestHandler<GetListSurveyQuery, GetListResponse<GetListSurveyListItemDto>>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public GetListSurveyQueryHandler(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSurveyListItemDto>> Handle(GetListSurveyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Survey> surveys = await _surveyRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSurveyListItemDto> response = _mapper.Map<GetListResponse<GetListSurveyListItemDto>>(surveys);
            return response;
        }
    }
}