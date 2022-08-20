using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Interfaces;

public interface IUserEndpoints
{
    Task<List<UserModel>> GetAll(string searchQuery = "");
    Task<UserModel> GetDetail(string id);
    Task Delete(string id);
    Task Create(RegisterDto model);
    Task Update(UserModel model);
}