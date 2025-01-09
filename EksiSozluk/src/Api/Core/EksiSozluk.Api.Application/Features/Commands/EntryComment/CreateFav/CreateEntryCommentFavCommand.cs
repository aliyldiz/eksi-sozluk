using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommand : IRequest<bool>
{
    public Guid EntryCommandId { get; set; }
    public Guid UserId { get; set; }
    
    public CreateEntryCommentFavCommand(Guid entryCommandId, Guid userId)
    {
        EntryCommandId = entryCommandId;
        UserId = userId;
    }
}