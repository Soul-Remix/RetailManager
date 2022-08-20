using System.ComponentModel.DataAnnotations;

namespace TypesLibrary.Shared.Models;

public class UserModel
{
    [StringLength(128)]
    public string Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string LastName { get; set; }

    [Required][EmailAddress] public string Email { get; set; }

    public List<string> Roles { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

