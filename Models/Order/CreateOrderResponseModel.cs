using Work_with_orders.Models.ProductModels.ViewModels;

namespace Work_with_orders.Models.Order;

public class CreateOrderResponseModel
{
    public List<ProductInOrderViewModel> ProductsOrderedSuccessfully { get; set; } = new List<ProductInOrderViewModel>();
    public List<ProductInOrderViewModel> ProductsOrderedFailed { get; set; } = new List<ProductInOrderViewModel>();
}