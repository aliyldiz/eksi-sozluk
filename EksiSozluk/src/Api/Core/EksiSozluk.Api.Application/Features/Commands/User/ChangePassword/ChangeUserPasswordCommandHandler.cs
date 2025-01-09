using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.Events.User;
using EksiSozluk.Common.Infrastructure;
using EksiSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace EksiSozluk.Api.Application.Features.Commands.User.ChangePassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;
    
    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));
        
        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);
        
        if (dbUser is null)
            throw new DatabaseValidationException("User not found");
        
        var encPass = PasswordEncryptor.Encrypt(request.OldPassword);
        if (dbUser.Password != encPass)
            throw new DatabaseValidationException("Old password is incorrect");

        dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);
        
        await _userRepository.UpdateAsync(dbUser);
        
        return true;
    }
}