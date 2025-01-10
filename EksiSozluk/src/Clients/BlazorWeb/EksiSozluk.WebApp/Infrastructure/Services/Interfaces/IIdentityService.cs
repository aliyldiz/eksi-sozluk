using EksiSozluk.Common.ViewModels.RequestModels;

namespace EksiSozluk.WebApp.Infrastructure.Services;

public interface IIdentityService
{
    bool IsLoggedIn { get; }
    string GetUserToken();
    string GetUserName();
    Guid GetUserId();
    Task<bool> Login(LoginUserCommand command);
    void Logout();
}