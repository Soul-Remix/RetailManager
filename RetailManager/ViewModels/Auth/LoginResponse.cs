namespace RetailManager.ViewModels.Auth;

public class LoginResponse
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryDate { get; set; }
}