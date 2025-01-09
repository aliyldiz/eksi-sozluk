using AutoMapper;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Common.ViewModels.Queries;
using EksiSozluk.Common.ViewModels.RequestModels;

namespace EksiSozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, LoginUserViewModel>()
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();
        
        CreateMap<UpdateUserCommand, User>();
        
        CreateMap<CreateEntryCommand, Entry>()
            .ReverseMap();

        CreateMap<CreateEntryCommentCommand, EntryComment>()
            .ReverseMap();
    }
}