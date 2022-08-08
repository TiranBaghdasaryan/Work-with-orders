namespace Work_with_orders.Entities;

public class BasketProduct : EntityBase<long>
{
    public Basket Basket { get; set; }
    public long BasketId { get; set; }

    public Product Product { get; set; }
    public long ProductId { get; set; }

    public int Quantity { get; set; }
}