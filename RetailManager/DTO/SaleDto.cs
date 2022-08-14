namespace RetailManager.DTO;

public class SaleDto
{
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public List<Cart> Cart { get; set; }
}

public class Cart
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}