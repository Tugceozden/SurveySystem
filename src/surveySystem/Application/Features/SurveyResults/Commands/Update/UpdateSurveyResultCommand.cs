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

namespace Application.Features.SurveyResults.Commands.Update;

public class UpdateSurveyResultCommand : IRequest<UpdatedSurveyResultResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid ParticipantId { get; set; }

    public string[] Roles => [Admin, Write, SurveyResultsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSurveyResults"];

    public class UpdateSurveyResultCommandHandler : IRequestHandler<UpdateSurveyResultCommand, UpdatedSurveyResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISurveyResultRepository _surveyResultRepository;
        private readonly SurveyResultBusinessRules _surveyResultBusinessRules;

        public UpdateSurveyResultCommandHandler(IMapper mapper, ISurveyResultRepository surveyResultRepository,
                                         SurveyResultBusinessRules surveyResultBusinessRules)
        {
            _mapper = mapper;
            _surveyResultRepository = surveyResultRepository;
            _surveyResultBusinessRules = surveyResultBusinessRules;
        }

        public async Task<UpdatedSurveyResultResponse> Handle(UpdateSurveyResultCommand request, CancellationToken cancellationToken)
        {
            SurveyResult? surveyResult = await _surveyResultRepository.GetAsync(predicate: sr => sr.Id == request.Id, cancellationToken: cancellationToken);
            await _surveyResultBusinessRules.SurveyResultShouldExistWhenSelected(surveyResult);
            surveyResult = _mapper.Map(request, surveyResult);

            await _surveyResultRepository.UpdateAsync(surveyResult!);

            UpdatedSurveyResultResponse response = _mapper.Map<UpdatedSurveyResultResponse>(surveyResult);
            return response;
        }
    }
}