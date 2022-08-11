using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.DTO;

public class ProductDto
{
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
}