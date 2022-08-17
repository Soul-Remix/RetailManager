using System.Net.Http.Json;
using Portal.Blazor.Interfaces;
using TypesLibrary.Shared.Dto;

namespace Portal.Blazor.Services;

public class UsersEndpoint : IUserEndpoints
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public UsersEndpoint(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task CreateUser(RegisterDto model)
    {
        var result = await _httpClient.PostAsJsonAsync(_config["endpoints:register"], model);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }
    }
}