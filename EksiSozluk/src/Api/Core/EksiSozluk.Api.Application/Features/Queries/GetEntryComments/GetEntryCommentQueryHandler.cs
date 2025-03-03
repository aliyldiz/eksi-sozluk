using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Infrastructure.Extensions;
using EksiSozluk.Common.Models.Page;
using EksiSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentViewModel>>
{
    private readonly IEntryCommentRepository _repository;

    public GetEntryCommentQueryHandler(IEntryCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedViewModel<GetEntryCommentViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        
        var query = _repository.AsQueryable();

        query = query.Include(i => i.EntryCommentFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryCommentVotes)
            .Where(i => i.EntryId == request.EntryId);

        var list = query.Select(i => new GetEntryCommentViewModel()
        {
            Id  = i.Id,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
            FavoriteCount = i.EntryCommentFavorites.Count,
            CreatedDate = i.CreateDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId)!.VoteType
                : Common.Models.VoteType.None
        });
        
        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}