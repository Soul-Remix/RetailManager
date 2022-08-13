using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.DTO;

public class SaleDto
{
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Tax { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Total { get; set; }

    public List<Cart> Cart { get; set; }
}

public class Cart
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}