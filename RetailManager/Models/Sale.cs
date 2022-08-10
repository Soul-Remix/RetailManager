using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("Sale")]
public class Sale
{
    public int Id { get; set; }
    
    [Column(TypeName = "datetime2")]
    public DateTime SaleDate { get; set; }
    
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal SubTotal { get; set; }
    
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Tax { get; set; }
    
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    public decimal Total { get; set; }
    
    [StringLength(128)]
    [ForeignKey("Account")]
    public string CashierId { get; set; }
    
    public Account Account { get; set; }
}