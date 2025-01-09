using EksiSozluk.Api.Application.Features.Queries.GetEntryComments;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Infrastructure.Extensions;
using EksiSozluk.Common.ViewModels.Page;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository _repository;

    public GetEntryDetailQueryHandler(IEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        
        var query = _repository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes)
            .Where(i => i.Id == request.EntryId);

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

        return await list.FirstOrDefaultAsync(cancellationToken);
    }
}