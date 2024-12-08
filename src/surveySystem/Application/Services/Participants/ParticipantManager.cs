using Application.Features.Participants.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Participants;

public class ParticipantManager : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;
    private readonly ParticipantBusinessRules _participantBusinessRules;

    public ParticipantManager(IParticipantRepository participantRepository, ParticipantBusinessRules participantBusinessRules)
    {
        _participantRepository = participantRepository;
        _participantBusinessRules = participantBusinessRules;
    }

    public async Task<Participant?> GetAsync(
        Expression<Func<Participant, bool>> predicate,
        Func<IQueryable<Participant>, IIncludableQueryable<Participant, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Participant? participant = await _participantRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return participant;
    }

    public async Task<IPaginate<Participant>?> GetListAsync(
        Expression<Func<Participant, bool>>? predicate = null,
        Func<IQueryable<Participant>, IOrderedQueryable<Participant>>? orderBy = null,
        Func<IQueryable<Participant>, IIncludableQueryable<Participant, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Participant> participantList = await _participantRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return participantList;
    }

    public async Task<Participant> AddAsync(Participant participant)
    {
        Participant addedParticipant = await _participantRepository.AddAsync(participant);

        return addedParticipant;
    }

    public async Task<Participant> UpdateAsync(Participant participant)
    {
        Participant updatedParticipant = await _participantRepository.UpdateAsync(participant);

        return updatedParticipant;
    }

    public async Task<Participant> DeleteAsync(Participant participant, bool permanent = false)
    {
        Participant deletedParticipant = await _participantRepository.DeleteAsync(participant);

        return deletedParticipant;
    }
}
