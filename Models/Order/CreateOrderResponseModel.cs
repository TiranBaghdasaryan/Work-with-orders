using Work_with_orders.Models.Product;

namespace Work_with_orders.Models.Order;

public class CreateOrderResponseModel
{
    public List<ProductInOrderViewModel> ProductsOrderedSuccessfully = new List<ProductInOrderViewModel>();
    public List<ProductInOrderViewModel> ProductsOrderedFailed = new List<ProductInOrderViewModel>();
}