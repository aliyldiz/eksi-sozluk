using EksiSozluk.Common;
using EksiSozluk.Common.Events.EntryComment;
using EksiSozluk.Common.Infrastructure;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommand : IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }

    public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}
