using Application.Features.Participants.Constants;
using Application.Features.Participants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Participants.Constants.ParticipantsOperationClaims;

namespace Application.Features.Participants.Queries.GetById;

public class GetByIdParticipantQuery : IRequest<GetByIdParticipantResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdParticipantQueryHandler : IRequestHandler<GetByIdParticipantQuery, GetByIdParticipantResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _participantRepository;
        private readonly ParticipantBusinessRules _participantBusinessRules;

        public GetByIdParticipantQueryHandler(IMapper mapper, IParticipantRepository participantRepository, ParticipantBusinessRules participantBusinessRules)
        {
            _mapper = mapper;
            _participantRepository = participantRepository;
            _participantBusinessRules = participantBusinessRules;
        }

        public async Task<GetByIdParticipantResponse> Handle(GetByIdParticipantQuery request, CancellationToken cancellationToken)
        {
            Participant? participant = await _participantRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _participantBusinessRules.ParticipantShouldExistWhenSelected(participant);

            GetByIdParticipantResponse response = _mapper.Map<GetByIdParticipantResponse>(participant);
            return response;
        }
    }
}