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

namespace Application.Features.Participants.Commands.Create;

public class CreateParticipantCommand : IRequest<CreatedParticipantResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public Guid UserID { get; set; }

    public string[] Roles => [Admin, Write, ParticipantsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetParticipants"];

    public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand, CreatedParticipantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _participantRepository;
        private readonly ParticipantBusinessRules _participantBusinessRules;

        public CreateParticipantCommandHandler(IMapper mapper, IParticipantRepository participantRepository,
                                         ParticipantBusinessRules participantBusinessRules)
        {
            _mapper = mapper;
            _participantRepository = participantRepository;
            _participantBusinessRules = participantBusinessRules;
        }

        public async Task<CreatedParticipantResponse> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
        {
            Participant participant = _mapper.Map<Participant>(request);

            await _participantRepository.AddAsync(participant);

            CreatedParticipantResponse response = _mapper.Map<CreatedParticipantResponse>(participant);
            return response;
        }
    }
}