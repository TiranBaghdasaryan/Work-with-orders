using Work_with_orders.Enums;

namespace Work_with_orders.Models.Product;

public class ProductViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }
}