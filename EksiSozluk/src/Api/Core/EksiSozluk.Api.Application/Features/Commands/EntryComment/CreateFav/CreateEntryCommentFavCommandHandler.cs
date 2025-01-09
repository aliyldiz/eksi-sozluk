using EksiSozluk.Common;
using EksiSozluk.Common.Events.EntryComment;
using EksiSozluk.Common.Infrastructure;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
       QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FaxExchangeName,
           exchangeType: SozlukConstants.DefaultExchangeType,
           queueName: SozlukConstants.CreateEntryCommentFavQueueName,
           obj: new CreateEntryCommentFavEvent() 
           {
               EntryCommentId = request.EntryCommandId,
               CreatedBy = request.UserId
           });

       return await Task.FromResult(true); // kod blogunda await işlemi yapılmadıgından .FromResult kullanıldı.
    }
}