using Work_with_orders.Models.ProductModels.ViewModels;

namespace Work_with_orders.Models.Order;

public class OrderDetailsViewModel
{
    public long Id { get; set; }
    public DateTime DoneDate { get; set; }
    public string Status { get; set; }

    public List<ProductInOrderViewModel> ProductsViewModels { get; set; } = new List<ProductInOrderViewModel>();
}