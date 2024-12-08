using Application.Features.Participations.Constants;
using Application.Features.Participations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Participations.Constants.ParticipationsOperationClaims;

namespace Application.Features.Participations.Queries.GetById;

public class GetByIdParticipationQuery : IRequest<GetByIdParticipationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdParticipationQueryHandler : IRequestHandler<GetByIdParticipationQuery, GetByIdParticipationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IParticipationRepository _participationRepository;
        private readonly ParticipationBusinessRules _participationBusinessRules;

        public GetByIdParticipationQueryHandler(IMapper mapper, IParticipationRepository participationRepository, ParticipationBusinessRules participationBusinessRules)
        {
            _mapper = mapper;
            _participationRepository = participationRepository;
            _participationBusinessRules = participationBusinessRules;
        }

        public async Task<GetByIdParticipationResponse> Handle(GetByIdParticipationQuery request, CancellationToken cancellationToken)
        {
            Participation? participation = await _participationRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _participationBusinessRules.ParticipationShouldExistWhenSelected(participation);

            GetByIdParticipationResponse response = _mapper.Map<GetByIdParticipationResponse>(participation);
            return response;
        }
    }
}