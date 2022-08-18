using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Interfaces;

public interface IProductsEndpoint
{
    Task<List<ProductModel>> GetAll();
}