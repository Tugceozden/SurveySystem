using Application.Features.Participants.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Participants.Constants.ParticipantsOperationClaims;

namespace Application.Features.Participants.Queries.GetList;

public class GetListParticipantQuery : IRequest<GetListResponse<GetListParticipantListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListParticipants({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetParticipants";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListParticipantQueryHandler : IRequestHandler<GetListParticipantQuery, GetListResponse<GetListParticipantListItemDto>>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public GetListParticipantQueryHandler(IParticipantRepository participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListParticipantListItemDto>> Handle(GetListParticipantQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Participant> participants = await _participantRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListParticipantListItemDto> response = _mapper.Map<GetListResponse<GetListParticipantListItemDto>>(participants);
            return response;
        }
    }
}