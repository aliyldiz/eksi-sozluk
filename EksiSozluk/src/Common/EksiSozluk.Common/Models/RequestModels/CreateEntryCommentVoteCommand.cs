using MediatR;

namespace EksiSozluk.Common.Models.RequestModels;

public class CreateEntryCommentVoteCommand : IRequest<Guid>, IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
    
    public CreateEntryCommentVoteCommand()
    {
        
    }
    
    public CreateEntryCommentVoteCommand(Guid entryCommentId, VoteType voteType, Guid createdBy)
    {
        EntryCommentId = entryCommentId;
        VoteType = voteType;
        CreatedBy = createdBy;
    }
}