using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("SaleDetail")]
public class SaleDetail
{
    public int Id { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal PurchasePrice { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Tax { get; set; } = 0;

    public int Quantity { get; set; } = 1;

    public int SaleId { get; set; }
    
    public Sale Sale { get; set; }

    public int ProductId { get; set; }
    
    public Product Product { get; set; }
}