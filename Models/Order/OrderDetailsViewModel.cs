using Work_with_orders.Models.Product;

namespace Work_with_orders.Models.Order;

public class OrderDetailsViewModel
{
    public long OrderId { get; set; }
    public DateTime DoneDate { get; set; }
    public string Status { get; set; }

    public List<ProductInOrderViewModel> ProductsViewModels = new List<ProductInOrderViewModel>();
}