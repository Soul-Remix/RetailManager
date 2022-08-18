using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Portal.Blazor.Interfaces;
using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Services;

public class ProductsEndpoint : IProductsEndpoint
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorage;

    public ProductsEndpoint(HttpClient client, IConfiguration config, ILocalStorageService localStorage)
    {
        _client = client;
        _config = config;
        _localStorage = localStorage;
    }

    public async Task<List<ProductModel>> GetAll()
    {
        var token = await _localStorage.GetItemAsync<string>(_config["token"]);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        var result = await _client.GetAsync(_config["endpoints:products"]);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(result.ReasonPhrase);
        }

        var resultStream = await result.Content.ReadAsStreamAsync();

        var data = await JsonSerializer.DeserializeAsync<List<ProductModel>>(resultStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return data;
    }
}