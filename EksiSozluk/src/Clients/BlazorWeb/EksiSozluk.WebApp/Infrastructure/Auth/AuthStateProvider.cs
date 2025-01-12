using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Blazored.LocalStorage;
using EksiSozluk.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace EksiSozluk.WebApp.Infrastructure.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); ;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var apiToken = await _localStorage.GetToken();

        if (string.IsNullOrEmpty(apiToken))
            return _anonymous;

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(apiToken);
        
        var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "jwtAuthType"));

        return new AuthenticationState(cp);
    }

    public void NotifyUserLogin(string userName, Guid userId)
    {
        var cp = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        }, "jwtAuthType"));
        
        var authState = Task.FromResult(new AuthenticationState(cp));
        
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}