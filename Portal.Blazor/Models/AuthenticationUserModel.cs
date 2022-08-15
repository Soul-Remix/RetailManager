using System.ComponentModel.DataAnnotations;

namespace Portal.Blazor.Models;

public class AuthenticationUserModel
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}