using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Portal.Blazor.Authentication.Interfaces;
using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly IConfiguration _config;

    public AuthenticationService(HttpClient httpClient,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authProvider,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authProvider = authProvider;
        _config = config;
    }

    public async Task<LoginResponse> LogIn(LoginDto model)
    {
        var data = new Dictionary<string, string>
        {
            { "email", model.Email },
            { "password", model.Password }
        };

        var result = await _httpClient.PostAsJsonAsync(
            _config["endpoints:login"],
            data
        );
        var resultContent = await result.Content.ReadAsStreamAsync();

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var resultData = await JsonSerializer.DeserializeAsync<LoginResponse>(resultContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsync(_config["token"], resultData.Token);

        ((AuthStateProvider)_authProvider).NotifyUserAuth(resultData.Token);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resultData.Token);

        return resultData;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(_config["token"]);
        ((AuthStateProvider)_authProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}