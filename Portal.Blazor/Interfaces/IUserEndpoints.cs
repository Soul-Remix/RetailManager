using TypesLibrary.Shared.Dto;

namespace Portal.Blazor.Interfaces;

public interface IUserEndpoints
{
    Task CreateUser(RegisterDto model);
}