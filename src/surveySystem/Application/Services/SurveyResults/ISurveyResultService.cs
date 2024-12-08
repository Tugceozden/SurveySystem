using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.SurveyResults;

public interface ISurveyResultService
{
    Task<SurveyResult?> GetAsync(
        Expression<Func<SurveyResult, bool>> predicate,
        Func<IQueryable<SurveyResult>, IIncludableQueryable<SurveyResult, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<SurveyResult>?> GetListAsync(
        Expression<Func<SurveyResult, bool>>? predicate = null,
        Func<IQueryable<SurveyResult>, IOrderedQueryable<SurveyResult>>? orderBy = null,
        Func<IQueryable<SurveyResult>, IIncludableQueryable<SurveyResult, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<SurveyResult> AddAsync(SurveyResult surveyResult);
    Task<SurveyResult> UpdateAsync(SurveyResult surveyResult);
    Task<SurveyResult> DeleteAsync(SurveyResult surveyResult, bool permanent = false);
}
