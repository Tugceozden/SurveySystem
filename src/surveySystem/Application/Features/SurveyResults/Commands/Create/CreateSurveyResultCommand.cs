using Application.Features.SurveyResults.Constants;
using Application.Features.SurveyResults.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SurveyResults.Constants.SurveyResultsOperationClaims;

namespace Application.Features.SurveyResults.Commands.Create;

public class CreateSurveyResultCommand : IRequest<CreatedSurveyResultResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }

    public string[] Roles => [Admin, Write, SurveyResultsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSurveyResults"];

    public class CreateSurveyResultCommandHandler : IRequestHandler<CreateSurveyResultCommand, CreatedSurveyResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly SurveyResultBusinessRules _surveyResultBusinessRules;

        public CreateSurveyResultCommandHandler(IMapper mapper, ISurveyResultRepository surveyResultRepository,
                                         SurveyResultBusinessRules surveyResultBusinessRules)
        {
            _mapper = mapper;
            _surveyResultRepository = surveyResultRepository;
            _surveyResultBusinessRules = surveyResultBusinessRules;
        }

        public async Task<CreatedSurveyResultResponse> Handle(CreateSurveyResultCommand request, CancellationToken cancellationToken)
        {
            SurveyResult surveyResult = _mapper.Map<SurveyResult>(request);

            await _surveyResultRepository.AddAsync(surveyResult);

            CreatedSurveyResultResponse response = _mapper.Map<CreatedSurveyResultResponse>(surveyResult);
            return response;
        }
    }
}