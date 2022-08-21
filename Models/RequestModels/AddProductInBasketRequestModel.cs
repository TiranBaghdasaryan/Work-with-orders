namespace Work_with_orders.Models.RequestModels;

public class AddProductInBasketRequestModel
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
}