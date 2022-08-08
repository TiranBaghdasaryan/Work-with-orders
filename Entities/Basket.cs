namespace Work_with_orders.Entities;

public class Basket : EntityBase<long>
{
    public DateTime CreatedDate { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<BasketProduct> BasketProduct { get; set; }

}