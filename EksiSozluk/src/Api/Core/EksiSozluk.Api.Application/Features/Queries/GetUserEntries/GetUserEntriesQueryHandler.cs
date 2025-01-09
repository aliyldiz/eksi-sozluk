using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Infrastructure.Extensions;
using EksiSozluk.Common.ViewModels.Page;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _repository;

    public GetUserEntriesQueryHandler(IEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.AsQueryable();

        if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            query = query.Where(i => i.CreatedById == request.UserId);

        else if (!string.IsNullOrEmpty(request.UserName))
            query = query.Where(i => i.CreatedBy.UserName == request.UserName);

        else
            return null;

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy);

        var list = query.Select(i => new GetUserEntriesDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = false,
            FavoriteCount = i.EntryFavorites.Count,
            CreatedDate = i.CreateDate,
            CreatedByUserName = i.CreatedBy.UserName,
        });
        
        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}