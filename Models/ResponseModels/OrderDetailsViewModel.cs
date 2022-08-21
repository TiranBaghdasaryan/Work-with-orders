namespace Work_with_orders.Models.ResponseModels;

public class OrderDetailsViewModel
{
    public long Id { get; set; }
    public DateTime DoneDate { get; set; }
    public string Status { get; set; }

    public List<ProductInOrderViewModel> ProductsViewModels { get; set; } = new List<ProductInOrderViewModel>();
}