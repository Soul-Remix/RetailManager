using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("Product")]
public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; }

    [Required]
    [StringLength(400, MinimumLength = 1)]
    public string Description { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal RetailPrice { get; set; }

    public int QuantityInStock { get; set; } = 1;

    public bool IsTaxable { get; set; } = false;

    [Column(TypeName = "datetime2")] public DateTime CreatedAt { get; set; }
    [Column(TypeName = "datetime2")] public DateTime UpdatedAt { get; set; }
}