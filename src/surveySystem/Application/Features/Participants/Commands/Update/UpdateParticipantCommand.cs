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

namespace Application.Features.Participants.Commands.Update;

public class UpdateParticipantCommand : IRequest<UpdatedParticipantResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public Guid UserID { get; set; }

    public string[] Roles => [Admin, Write, ParticipantsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParticipants"];

    public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand, UpdatedParticipantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _participantRepository;
        private readonly ParticipantBusinessRules _participantBusinessRules;

        public UpdateParticipantCommandHandler(IMapper mapper, IParticipantRepository participantRepository,
                                         ParticipantBusinessRules participantBusinessRules)
        {
            _mapper = mapper;
            _participantRepository = participantRepository;
            _participantBusinessRules = participantBusinessRules;
        }

        public async Task<UpdatedParticipantResponse> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            Participant? participant = await _participantRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _participantBusinessRules.ParticipantShouldExistWhenSelected(participant);
            participant = _mapper.Map(request, participant);

            await _participantRepository.UpdateAsync(participant!);

            UpdatedParticipantResponse response = _mapper.Map<UpdatedParticipantResponse>(participant);
            return response;
        }
    }
}