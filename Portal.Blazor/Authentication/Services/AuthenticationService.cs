using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Portal.Blazor.Authentication.Interfaces;
using Portal.Blazor.Authentication.Models;

namespace Portal.Blazor.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authProvider;

    public AuthenticationService(HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authProvider = authProvider;
    }

    public async Task<AuthenticatedUserModel> LogIn(AuthenticationUserModel model)
    {
        var data = new Dictionary<string, string>
        {
            { "email", model.Email },
            { "password", model.Password }
        };

        var result = await _httpClient.PostAsJsonAsync(
            "api/auth/login",
            data
        );
        var resultContent = await result.Content.ReadAsStreamAsync();

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var resultData = await JsonSerializer.DeserializeAsync<AuthenticatedUserModel>(resultContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync("token", resultData.Token);

        ((AuthStateProvider)_authProvider).NotifyUserAuth(resultData.Token);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resultData.Token);

        return resultData;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("token");
        ((AuthStateProvider)_authProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}