using System.Net.Http.Headers;
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
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("email", model.Email),
            new KeyValuePair<string, string>("password", model.Password)
        });

        var result = await _httpClient.PostAsync(
            "api/auth/login",
            data
        );
        var resultContent = await result.Content.ReadAsStreamAsync();

        Console.WriteLine(result.IsSuccessStatusCode);

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