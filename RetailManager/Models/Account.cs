using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("Account")]
public class Account
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(128)]
    [Key]
    public string Id { get; set; }
    
    [Required]
    [StringLength(50,MinimumLength = 1)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50,MinimumLength = 1)]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Column(TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; }
    [Column(TypeName = "datetime2")]
    public DateTime UpdatedAt { get; set; }
}