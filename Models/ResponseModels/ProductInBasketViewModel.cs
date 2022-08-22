
namespace Work_with_orders.Models.ResponseModels;

public class ProductInBasketViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int Quantity { get; set; }
}