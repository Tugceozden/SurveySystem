using Application.Features.Participants.Constants;
using Application.Features.Participants.Constants;
using Application.Features.Participants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Participants.Constants.ParticipantsOperationClaims;

namespace Application.Features.Participants.Commands.Delete;

public class DeleteParticipantCommand : IRequest<DeletedParticipantResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ParticipantsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParticipants"];

    public class DeleteParticipantCommandHandler : IRequestHandler<DeleteParticipantCommand, DeletedParticipantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _participantRepository;
        private readonly ParticipantBusinessRules _participantBusinessRules;

        public DeleteParticipantCommandHandler(IMapper mapper, IParticipantRepository participantRepository,
                                         ParticipantBusinessRules participantBusinessRules)
        {
            _mapper = mapper;
            _participantRepository = participantRepository;
            _participantBusinessRules = participantBusinessRules;
        }

        public async Task<DeletedParticipantResponse> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
        {
            Participant? participant = await _participantRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _participantBusinessRules.ParticipantShouldExistWhenSelected(participant);

            await _participantRepository.DeleteAsync(participant!);

            DeletedParticipantResponse response = _mapper.Map<DeletedParticipantResponse>(participant);
            return response;
        }
    }
}