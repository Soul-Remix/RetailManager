using System.ComponentModel.DataAnnotations;

namespace RetailManager.DTO;

public class ProfileDto
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string LastName { get; set; }
}