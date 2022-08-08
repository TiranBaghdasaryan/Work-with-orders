using Work_with_orders.Enums;

namespace Work_with_orders.Entities;

public class Product : EntityBase<long>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }

    public ICollection<OrderProduct> OrderProduct { get; set; }
    public ICollection<BasketProduct> BasketProduct { get; set; }
}