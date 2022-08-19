using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Interfaces;

public interface IProductsEndpoint
{
    Task<List<ProductModel>> GetAll(string searchQuery = "");
    Task<ProductModel> GetDetail(int id);
    Task Delete(int id);
    Task<ProductModel> Create(ProductModel model);
    Task Update(ProductModel model);
}