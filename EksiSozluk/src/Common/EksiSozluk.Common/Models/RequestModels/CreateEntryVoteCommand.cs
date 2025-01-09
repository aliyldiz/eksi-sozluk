using MediatR;

namespace EksiSozluk.Common.ViewModels.RequestModels;

public class CreateEntryVoteCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}