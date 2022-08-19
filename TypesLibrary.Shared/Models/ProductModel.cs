using System.ComponentModel.DataAnnotations;

namespace TypesLibrary.Shared.Models;

public class ProductModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; }

    [Required]
    [StringLength(400, MinimumLength = 1)]
    public string Description { get; set; }

    [DataType(DataType.Currency)] public decimal RetailPrice { get; set; }

    public int QuantityInStock { get; set; } = 1;
    public bool IsTaxable { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}