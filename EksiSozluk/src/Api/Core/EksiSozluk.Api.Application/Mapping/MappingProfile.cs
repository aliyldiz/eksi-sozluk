using AutoMapper;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Common.ViewModels.Queries;

namespace EksiSozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();
    }
}