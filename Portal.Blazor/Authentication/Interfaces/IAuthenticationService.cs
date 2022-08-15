using Portal.Blazor.Models;

namespace Portal.Blazor.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel> LogIn(AuthenticationUserModel model);
    Task Logout();
}