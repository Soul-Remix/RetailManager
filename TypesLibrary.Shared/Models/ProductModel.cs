namespace TypesLibrary.Shared.Models;

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal RetailPrice { get; set; }
    public int QuantityInStock { get; set; } = 1;
    public bool IsTaxable { get; set; } = false;
    public bool IsArchived { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}