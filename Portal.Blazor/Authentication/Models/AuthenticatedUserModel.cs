namespace Portal.Blazor.Authentication.Models;

public class AuthenticatedUserModel
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryDate { get; set; }
}