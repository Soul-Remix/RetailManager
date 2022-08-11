using RetailManager.DTO.Auth;
using RetailManager.ViewModels.Auth;

namespace RetailManager.Interfaces;

public interface IAuthService
{
    public Task RegisterUserAsync(RegisterDto model);
    public Task<LoginResponse> LoginUserAsync(LoginDto model);
}