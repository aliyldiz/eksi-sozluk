using AutoMapper;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common;
using EksiSozluk.Common.Events.User;
using EksiSozluk.Common.Infrastructure;
using EksiSozluk.Common.Infrastructure.Exceptions;
using EksiSozluk.Common.ViewModels.RequestModels;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
        
        if (existsUser is not null)
            throw new DatabaseValidationException("User already exists");
        
        var dbUser = _mapper.Map<Domain.Models.User>(request);
        
        var rows = await _userRepository.AddAsync(dbUser);
        
        if (rows > 0)
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
        }
        
        return dbUser.Id;
    }
}