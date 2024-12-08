using Application.Features.Participations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Participations.Rules;

public class ParticipationBusinessRules : BaseBusinessRules
{
    private readonly IParticipationRepository _participationRepository;
    private readonly ILocalizationService _localizationService;

    public ParticipationBusinessRules(IParticipationRepository participationRepository, ILocalizationService localizationService)
    {
        _participationRepository = participationRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ParticipationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ParticipationShouldExistWhenSelected(Participation? participation)
    {
        if (participation == null)
            await throwBusinessException(ParticipationsBusinessMessages.ParticipationNotExists);
    }

    public async Task ParticipationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Participation? participation = await _participationRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ParticipationShouldExistWhenSelected(participation);
    }
}