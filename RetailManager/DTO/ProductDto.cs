using System.ComponentModel.DataAnnotations;

namespace RetailManager.DTO;

public class ProductDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; }

    [Required]
    [StringLength(400, MinimumLength = 1)]
    public string Description { get; set; }

    public decimal RetailPrice { get; set; }
    public int QuantityInStock { get; set; }
    public bool IsTaxable { get; set; }
    public bool IsArchived { get; set; }
}