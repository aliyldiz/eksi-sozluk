using AutoMapper;
using EksiSozluk.Api.Domain.Models;
using EksiSozluk.Common.Models.Queries;
using EksiSozluk.Common.Models.RequestModels;

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

        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, 
                y => y.MapFrom(a => a.EntryComments.Count));

        CreateMap<CreateEntryCommentCommand, EntryComment>()
            .ReverseMap();
    }
}