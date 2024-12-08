using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ISurveyResultRepository : IAsyncRepository<SurveyResult, Guid>, IRepository<SurveyResult, Guid>
{
}