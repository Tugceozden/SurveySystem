using Application.Features.SurveyResults.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SurveyResults;

public class SurveyResultManager : ISurveyResultService
{
    private readonly ISurveyResultRepository _surveyResultRepository;
    private readonly SurveyResultBusinessRules _surveyResultBusinessRules;

    public SurveyResultManager(ISurveyResultRepository surveyResultRepository, SurveyResultBusinessRules surveyResultBusinessRules)
    {
        _surveyResultRepository = surveyResultRepository;
        _surveyResultBusinessRules = surveyResultBusinessRules;
    }

    public async Task<SurveyResult?> GetAsync(
        Expression<Func<SurveyResult, bool>> predicate,
        Func<IQueryable<SurveyResult>, IIncludableQueryable<SurveyResult, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        SurveyResult? surveyResult = await _surveyResultRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return surveyResult;
    }

    public async Task<IPaginate<SurveyResult>?> GetListAsync(
        Expression<Func<SurveyResult, bool>>? predicate = null,
        Func<IQueryable<SurveyResult>, IOrderedQueryable<SurveyResult>>? orderBy = null,
        Func<IQueryable<SurveyResult>, IIncludableQueryable<SurveyResult, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<SurveyResult> surveyResultList = await _surveyResultRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return surveyResultList;
    }

    public async Task<SurveyResult> AddAsync(SurveyResult surveyResult)
    {
        SurveyResult addedSurveyResult = await _surveyResultRepository.AddAsync(surveyResult);

        return addedSurveyResult;
    }

    public async Task<SurveyResult> UpdateAsync(SurveyResult surveyResult)
    {
        SurveyResult updatedSurveyResult = await _surveyResultRepository.UpdateAsync(surveyResult);

        return updatedSurveyResult;
    }

    public async Task<SurveyResult> DeleteAsync(SurveyResult surveyResult, bool permanent = false)
    {
        SurveyResult deletedSurveyResult = await _surveyResultRepository.DeleteAsync(surveyResult);

        return deletedSurveyResult;
    }
}
