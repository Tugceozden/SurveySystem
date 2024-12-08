using Application.Features.Participations.Constants;
using Application.Features.Participations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Participations.Constants.ParticipationsOperationClaims;

namespace Application.Features.Participations.Commands.Update;

public class UpdateParticipationCommand : IRequest<UpdatedParticipationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid PaticipantId { get; set; }
    public Guid SurveyId { get; set; }
    public string? Answers { get; set; }

    public string[] Roles => [Admin, Write, ParticipationsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParticipations"];

    public class UpdateParticipationCommandHandler : IRequestHandler<UpdateParticipationCommand, UpdatedParticipationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipationRepository _participationRepository;
        private readonly ParticipationBusinessRules _participationBusinessRules;

        public UpdateParticipationCommandHandler(IMapper mapper, IParticipationRepository participationRepository,
                                         ParticipationBusinessRules participationBusinessRules)
        {
            _mapper = mapper;
            _participationRepository = participationRepository;
            _participationBusinessRules = participationBusinessRules;
        }

        public async Task<UpdatedParticipationResponse> Handle(UpdateParticipationCommand request, CancellationToken cancellationToken)
        {
            Participation? participation = await _participationRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _participationBusinessRules.ParticipationShouldExistWhenSelected(participation);
            participation = _mapper.Map(request, participation);

            await _participationRepository.UpdateAsync(participation!);

            UpdatedParticipationResponse response = _mapper.Map<UpdatedParticipationResponse>(participation);
            return response;
        }
    }
}