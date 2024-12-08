using Application.Features.SurveyResults.Constants;
using Application.Features.SurveyResults.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SurveyResults.Constants.SurveyResultsOperationClaims;

namespace Application.Features.SurveyResults.Queries.GetById;

public class GetByIdSurveyResultQuery : IRequest<GetByIdSurveyResultResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSurveyResultQueryHandler : IRequestHandler<GetByIdSurveyResultQuery, GetByIdSurveyResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly SurveyResultBusinessRules _surveyResultBusinessRules;

        public GetByIdSurveyResultQueryHandler(IMapper mapper, ISurveyResultRepository surveyResultRepository, SurveyResultBusinessRules surveyResultBusinessRules)
        {
            _mapper = mapper;
            _surveyResultRepository = surveyResultRepository;
            _surveyResultBusinessRules = surveyResultBusinessRules;
        }

        public async Task<GetByIdSurveyResultResponse> Handle(GetByIdSurveyResultQuery request, CancellationToken cancellationToken)
        {
            SurveyResult? surveyResult = await _surveyResultRepository.GetAsync(predicate: sr => sr.Id == request.Id, cancellationToken: cancellationToken);
            await _surveyResultBusinessRules.SurveyResultShouldExistWhenSelected(surveyResult);

            GetByIdSurveyResultResponse response = _mapper.Map<GetByIdSurveyResultResponse>(surveyResult);
            return response;
        }
    }
}