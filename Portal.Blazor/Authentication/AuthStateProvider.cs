using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Portal.Blazor.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage, IConfiguration config)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _config = config;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);

        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymous;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(
            new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }

    public void NotifyUserAuth(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(
            new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}