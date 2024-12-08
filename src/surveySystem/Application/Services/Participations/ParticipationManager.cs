using Application.Features.Participations.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Participations;

public class ParticipationManager : IParticipationService
{
    private readonly IParticipationRepository _participationRepository;
    private readonly ParticipationBusinessRules _participationBusinessRules;

    public ParticipationManager(IParticipationRepository participationRepository, ParticipationBusinessRules participationBusinessRules)
    {
        _participationRepository = participationRepository;
        _participationBusinessRules = participationBusinessRules;
    }

    public async Task<Participation?> GetAsync(
        Expression<Func<Participation, bool>> predicate,
        Func<IQueryable<Participation>, IIncludableQueryable<Participation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Participation? participation = await _participationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return participation;
    }

    public async Task<IPaginate<Participation>?> GetListAsync(
        Expression<Func<Participation, bool>>? predicate = null,
        Func<IQueryable<Participation>, IOrderedQueryable<Participation>>? orderBy = null,
        Func<IQueryable<Participation>, IIncludableQueryable<Participation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Participation> participationList = await _participationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return participationList;
    }

    public async Task<Participation> AddAsync(Participation participation)
    {
        Participation addedParticipation = await _participationRepository.AddAsync(participation);

        return addedParticipation;
    }

    public async Task<Participation> UpdateAsync(Participation participation)
    {
        Participation updatedParticipation = await _participationRepository.UpdateAsync(participation);

        return updatedParticipation;
    }

    public async Task<Participation> DeleteAsync(Participation participation, bool permanent = false)
    {
        Participation deletedParticipation = await _participationRepository.DeleteAsync(participation);

        return deletedParticipation;
    }
}
