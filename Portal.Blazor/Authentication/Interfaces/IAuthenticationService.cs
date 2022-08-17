using TypesLibrary.Shared.Dto;
using TypesLibrary.Shared.Models;

namespace Portal.Blazor.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> LogIn(LoginDto model);
    Task Logout();
}