using Application.Features.Participants.Commands.Create;
using Application.Features.Participants.Commands.Delete;
using Application.Features.Participants.Commands.Update;
using Application.Features.Participants.Queries.GetById;
using Application.Features.Participants.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Participants.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Participant, CreateParticipantCommand>().ReverseMap();
        CreateMap<Participant, CreatedParticipantResponse>().ReverseMap();
        CreateMap<Participant, UpdateParticipantCommand>().ReverseMap();
        CreateMap<Participant, UpdatedParticipantResponse>().ReverseMap();
        CreateMap<Participant, DeleteParticipantCommand>().ReverseMap();
        CreateMap<Participant, DeletedParticipantResponse>().ReverseMap();
        CreateMap<Participant, GetByIdParticipantResponse>().ReverseMap();
        CreateMap<Participant, GetListParticipantListItemDto>().ReverseMap();
        CreateMap<IPaginate<Participant>, GetListResponse<GetListParticipantListItemDto>>().ReverseMap();
    }
}