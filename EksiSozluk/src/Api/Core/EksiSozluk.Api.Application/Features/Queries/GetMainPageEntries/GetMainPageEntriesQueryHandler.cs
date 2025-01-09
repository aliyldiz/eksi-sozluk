using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Infrastructure.Extensions;
using EksiSozluk.Common.ViewModels.Page;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _repository;

    public GetMainPageEntriesQueryHandler(IEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes);

        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id  = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoriteCount = i.EntryFavorites.Count,
            CreatedDate = i.CreateDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId)!.VoteType
                : Common.ViewModels.VoteType.None
        });
        
        var entries = await list.GetPaged(request.Page, request.PageSize);

        return new PagedViewModel<GetEntryDetailViewModel>(entries.Result, entries.PageInfo);
    }
}