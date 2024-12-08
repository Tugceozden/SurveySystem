using Application.Features.Participations.Commands.Create;
using Application.Features.Participations.Commands.Delete;
using Application.Features.Participations.Commands.Update;
using Application.Features.Participations.Queries.GetById;
using Application.Features.Participations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Participations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Participation, CreateParticipationCommand>().ReverseMap();
        CreateMap<Participation, CreatedParticipationResponse>().ReverseMap();
        CreateMap<Participation, UpdateParticipationCommand>().ReverseMap();
        CreateMap<Participation, UpdatedParticipationResponse>().ReverseMap();
        CreateMap<Participation, DeleteParticipationCommand>().ReverseMap();
        CreateMap<Participation, DeletedParticipationResponse>().ReverseMap();
        CreateMap<Participation, GetByIdParticipationResponse>().ReverseMap();
        CreateMap<Participation, GetListParticipationListItemDto>().ReverseMap();
        CreateMap<IPaginate<Participation>, GetListResponse<GetListParticipationListItemDto>>().ReverseMap();
    }
}