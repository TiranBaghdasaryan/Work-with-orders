namespace Work_with_orders.Entities;

public class Product : EntityBase<long>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
}