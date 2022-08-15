using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailManager.Models;

[Table("Sale")]
public class Sale
{
    public int Id { get; set; }

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
    [ForeignKey("Profile")]
    public string CashierId { get; set; }

    public Profile Profile { get; set; }
}