using Application.Features.SurveyResults.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.SurveyResults.Rules;

public class SurveyResultBusinessRules : BaseBusinessRules
{
    private readonly ISurveyResultRepository _surveyResultRepository;
    private readonly ILocalizationService _localizationService;

    public SurveyResultBusinessRules(ISurveyResultRepository surveyResultRepository, ILocalizationService localizationService)
    {
        _surveyResultRepository = surveyResultRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SurveyResultsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SurveyResultShouldExistWhenSelected(SurveyResult? surveyResult)
    {
        if (surveyResult == null)
            await throwBusinessException(SurveyResultsBusinessMessages.SurveyResultNotExists);
    }

    public async Task SurveyResultIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SurveyResult? surveyResult = await _surveyResultRepository.GetAsync(
            predicate: sr => sr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SurveyResultShouldExistWhenSelected(surveyResult);
    }
}