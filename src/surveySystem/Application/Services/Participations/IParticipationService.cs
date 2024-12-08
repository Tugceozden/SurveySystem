using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Participations;

public interface IParticipationService
{
    Task<Participation?> GetAsync(
        Expression<Func<Participation, bool>> predicate,
        Func<IQueryable<Participation>, IIncludableQueryable<Participation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Participation>?> GetListAsync(
        Expression<Func<Participation, bool>>? predicate = null,
        Func<IQueryable<Participation>, IOrderedQueryable<Participation>>? orderBy = null,
        Func<IQueryable<Participation>, IIncludableQueryable<Participation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Participation> AddAsync(Participation participation);
    Task<Participation> UpdateAsync(Participation participation);
    Task<Participation> DeleteAsync(Participation participation, bool permanent = false);
}
