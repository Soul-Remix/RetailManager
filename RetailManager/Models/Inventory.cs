using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("Inventory")]
public class Inventory
{
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal PurchasePrice { get; set; }
    
    [Column(TypeName = "datetime2")]
    public DateTime PurchaseDate { get; set; }
    
    public int ProductId { get; set; }
    
    public Product Product { get; set; }
}