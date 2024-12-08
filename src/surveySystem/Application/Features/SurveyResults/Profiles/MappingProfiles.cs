using Application.Features.SurveyResults.Commands.Create;
using Application.Features.SurveyResults.Commands.Delete;
using Application.Features.SurveyResults.Commands.Update;
using Application.Features.SurveyResults.Queries.GetById;
using Application.Features.SurveyResults.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.SurveyResults.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SurveyResult, CreateSurveyResultCommand>().ReverseMap();
        CreateMap<SurveyResult, CreatedSurveyResultResponse>().ReverseMap();
        CreateMap<SurveyResult, UpdateSurveyResultCommand>().ReverseMap();
        CreateMap<SurveyResult, UpdatedSurveyResultResponse>().ReverseMap();
        CreateMap<SurveyResult, DeleteSurveyResultCommand>().ReverseMap();
        CreateMap<SurveyResult, DeletedSurveyResultResponse>().ReverseMap();
        CreateMap<SurveyResult, GetByIdSurveyResultResponse>().ReverseMap();
        CreateMap<SurveyResult, GetListSurveyResultListItemDto>().ReverseMap();
        CreateMap<IPaginate<SurveyResult>, GetListResponse<GetListSurveyResultListItemDto>>().ReverseMap();
    }
}