namespace Work_with_orders.Models.ResponseModels;

public class CreateOrderResponseModel
{
    public List<ProductInOrderViewModel> ProductsOrderedSuccessfully { get; set; } = new List<ProductInOrderViewModel>();
    public List<ProductInOrderViewModel> ProductsOrderedFailed { get; set; } = new List<ProductInOrderViewModel>();
}