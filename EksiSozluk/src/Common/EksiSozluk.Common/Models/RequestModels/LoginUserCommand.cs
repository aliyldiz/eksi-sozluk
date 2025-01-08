using EksiSozluk.Common.ViewModels.Queries;
using MediatR;

namespace EksiSozluk.Common.ViewModels.RequestModels;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public LoginUserCommand()
    {
        
    }
    
    public LoginUserCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }
    
    public string EmailAddress { get; set; }
    public string Password { get; set; }
}