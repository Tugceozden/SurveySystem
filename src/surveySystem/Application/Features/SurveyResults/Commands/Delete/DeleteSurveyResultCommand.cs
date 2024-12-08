using Application.Features.SurveyResults.Constants;
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

namespace Application.Features.SurveyResults.Commands.Delete;

public class DeleteSurveyResultCommand : IRequest<DeletedSurveyResultResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, SurveyResultsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSurveyResults"];

    public class DeleteSurveyResultCommandHandler : IRequestHandler<DeleteSurveyResultCommand, DeletedSurveyResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly SurveyResultBusinessRules _surveyResultBusinessRules;

        public DeleteSurveyResultCommandHandler(IMapper mapper, ISurveyResultRepository surveyResultRepository,
                                         SurveyResultBusinessRules surveyResultBusinessRules)
        {
            _mapper = mapper;
            _surveyResultRepository = surveyResultRepository;
            _surveyResultBusinessRules = surveyResultBusinessRules;
        }

        public async Task<DeletedSurveyResultResponse> Handle(DeleteSurveyResultCommand request, CancellationToken cancellationToken)
        {
            SurveyResult? surveyResult = await _surveyResultRepository.GetAsync(predicate: sr => sr.Id == request.Id, cancellationToken: cancellationToken);
            await _surveyResultBusinessRules.SurveyResultShouldExistWhenSelected(surveyResult);

            await _surveyResultRepository.DeleteAsync(surveyResult!);

            DeletedSurveyResultResponse response = _mapper.Map<DeletedSurveyResultResponse>(surveyResult);
            return response;
        }
    }
}