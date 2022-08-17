using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

namespace RetailManager.Interfaces;

public interface IAuthService
{
    public Task RegisterUserAsync(RegisterDto model);
    public Task<LoginResponse> LoginUserAsync(LoginDto model);
}