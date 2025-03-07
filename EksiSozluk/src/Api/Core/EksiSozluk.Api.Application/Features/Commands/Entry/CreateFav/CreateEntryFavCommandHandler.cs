using EksiSozluk.Common;
using EksiSozluk.Common.Events.Entry;
using EksiSozluk.Common.Infrastructure;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryFavQueueName,
            obj: new CreateEntryFavEvent() 
            {
                EntryId = request.EntryId.Value,
                CreatedBy = request.UserId.Value
            });
        
        return Task.FromResult(true);
    }
}