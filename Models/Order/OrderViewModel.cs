namespace Work_with_orders.Models.Order;

public class OrderViewModel
{
    public long Id { get; set; }
    public DateTime DoneDate { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}