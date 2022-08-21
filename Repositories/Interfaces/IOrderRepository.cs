using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    public Task<List<Order>> GetOrdersByUserId(long userId);
    public Task<Order> GetOrderById(long orderId);
}