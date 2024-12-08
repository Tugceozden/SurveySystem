using Application.Features.Participations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Participations.Constants.ParticipationsOperationClaims;

namespace Application.Features.Participations.Queries.GetList;

public class GetListParticipationQuery : IRequest<GetListResponse<GetListParticipationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListParticipations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetParticipations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListParticipationQueryHandler : IRequestHandler<GetListParticipationQuery, GetListResponse<GetListParticipationListItemDto>>
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IMapper _mapper;

        public GetListParticipationQueryHandler(IParticipationRepository participationRepository, IMapper mapper)
        {
            _participationRepository = participationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListParticipationListItemDto>> Handle(GetListParticipationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Participation> participations = await _participationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListParticipationListItemDto> response = _mapper.Map<GetListResponse<GetListParticipationListItemDto>>(participations);
            return response;
        }
    }
}