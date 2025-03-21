using EksiSozluk.Common.Models.Page;
using EksiSozluk.Common.Models.Queries;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery : BasePageQuery, IRequest<PagedViewModel<GetEntryCommentViewModel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}