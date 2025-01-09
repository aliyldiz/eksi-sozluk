using AutoMapper;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common;
using EksiSozluk.Common.Events.User;
using EksiSozluk.Common.Infrastructure;
using EksiSozluk.Common.Infrastructure.Exceptions;
using EksiSozluk.Common.ViewModels.RequestModels;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.User.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetByIdAsync(request.Id);
        
        if (dbUser is null)
            throw new DatabaseValidationException("User not found");
        
        var dbEmailAddress = dbUser.EmailAddress;
        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;
        
        _mapper.Map(request, dbUser);
        
        var rows = await _userRepository.UpdateAsync(dbUser);
        
        if (emailChanged && rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };
            
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType, 
                queueName: SozlukConstants.UserEmailChangedQueueName, 
                obj: @event);
            
            dbUser.EmailConfirmed = false;
            await _userRepository.UpdateAsync(dbUser);
        }
        
        return dbUser.Id;
    }
}