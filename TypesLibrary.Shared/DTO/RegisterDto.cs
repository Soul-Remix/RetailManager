using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TypesLibrary.Shared.Dto;

public class RegisterDto
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}