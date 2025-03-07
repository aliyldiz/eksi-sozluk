using EksiSozluk.Common;
using EksiSozluk.Common.Events.EntryComment;
using EksiSozluk.Common.Infrastructure;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
            obj: new DeleteEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId,
            });
        
        return await Task.FromResult(true);
    }
}