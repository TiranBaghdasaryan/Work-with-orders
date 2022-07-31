namespace Work_with_orders.Entities;

public class OrderProduct
{
    public Order Order { get; set; }
    public long OrderId { get; set; }

    public Product Product { get; set; }
    public long ProductId { get; set; }

    public int Quantity { get; set; }
}