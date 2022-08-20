using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Portal.Blazor.Interfaces;
using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Services;

public class UsersEndpoint : IUserEndpoints
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorage;

    public UsersEndpoint(HttpClient httpClient, IConfiguration config, ILocalStorageService localStorage)
    {
        _client = httpClient;
        _config = config;
        _localStorage = localStorage;
    }

    public async Task<List<UserModel>> GetAll(string searchQuery = "")
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var result = await _client.GetAsync(_config["endpoints:users"]);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }

        var resultStream = await result.Content.ReadAsStreamAsync();

        var data = await JsonSerializer.DeserializeAsync<List<UserModel>>(resultStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return data;
    }

    public async Task<UserModel> GetDetail(string id)
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var result = await _client.GetAsync($"{_config["endpoints:users"]}/{id}");

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }

        var resultStream = await result.Content.ReadAsStreamAsync();

        var data = await JsonSerializer.DeserializeAsync<UserModel>(resultStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return data;
    }
    public async Task Create(RegisterDto model)
    {
        var result = await _client.PostAsJsonAsync(_config["endpoints:register"], model);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }
    }

    public async Task Update(UserModel model)
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var result = await _client.PutAsJsonAsync($"{_config["endpoints:users"]}/{model.Id}", model);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }
    }
    public async Task Delete(string id)
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var result = await _client.DeleteAsync($"{_config["endpoints:users"]}/{id}");

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }
    }
}