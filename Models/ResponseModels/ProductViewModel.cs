using Work_with_orders.Enums;

namespace Work_with_orders.Models.ResponseModels;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }
}