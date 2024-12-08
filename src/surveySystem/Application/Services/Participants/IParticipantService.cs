using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Participants;

public interface IParticipantService
{
    Task<Participant?> GetAsync(
        Expression<Func<Participant, bool>> predicate,
        Func<IQueryable<Participant>, IIncludableQueryable<Participant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Participant>?> GetListAsync(
        Expression<Func<Participant, bool>>? predicate = null,
        Func<IQueryable<Participant>, IOrderedQueryable<Participant>>? orderBy = null,
        Func<IQueryable<Participant>, IIncludableQueryable<Participant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Participant> AddAsync(Participant participant);
    Task<Participant> UpdateAsync(Participant participant);
    Task<Participant> DeleteAsync(Participant participant, bool permanent = false);
}
