using Work_with_orders.Enums;

namespace Work_with_orders.Entities;

public class Order : EntityBase<long>
{
    public DateTime DoneDate { get; set; }
    public OrderStatus Status { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

    public ICollection<OrderProduct> OrderProduct { get; set; }

    public Order()
    {
        Status = OrderStatus.New;
        DoneDate = DateTime.UtcNow;
    }

}