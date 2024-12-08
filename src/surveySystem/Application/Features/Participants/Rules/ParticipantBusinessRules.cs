using Application.Features.Participants.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Participants.Rules;

public class ParticipantBusinessRules : BaseBusinessRules
{
    private readonly IParticipantRepository _participantRepository;
    private readonly ILocalizationService _localizationService;

    public ParticipantBusinessRules(IParticipantRepository participantRepository, ILocalizationService localizationService)
    {
        _participantRepository = participantRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ParticipantsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ParticipantShouldExistWhenSelected(Participant? participant)
    {
        if (participant == null)
            await throwBusinessException(ParticipantsBusinessMessages.ParticipantNotExists);
    }

    public async Task ParticipantIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Participant? participant = await _participantRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ParticipantShouldExistWhenSelected(participant);
    }
}