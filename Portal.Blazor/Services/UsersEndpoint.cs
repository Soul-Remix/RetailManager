using System.Net.Http.Json;
using Portal.Blazor.Interfaces;
using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

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

    public Task<List<UserModel>> GetAll(string searchQuery = "")
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> GetDetail(string id)
    {
        throw new NotImplementedException();
    }
    public async Task Create(RegisterDto model)
    {
        var result = await _httpClient.PostAsJsonAsync(_config["endpoints:register"], model);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }
    }

    public Task Update(UserModel model)
    {
        throw new NotImplementedException();
    }
    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }
}