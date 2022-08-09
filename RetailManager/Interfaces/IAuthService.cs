using RetailManager.DTO.Auth;
using RetailManager.ViewModels.Auth;

namespace RetailManager.Interfaces;

public interface IAuthService
{
    public Task RegisterUserAsync(RegisterDTO model);
    public Task<LoginResponse> LoginUserAsync(LoginDTO model);
}