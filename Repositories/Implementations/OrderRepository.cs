using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Interfaces;

namespace Work_with_orders.Repositories.Implementations;

public class OrderRepository : GenericRepository<Order> , IOrderRepository
{
    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<List<Order>> GetOrdersByUserId(long userId)
    {
        var orders = await _db.Where(x => x.UserId == userId).ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderById(long orderId)
    {
        var order = await _db.FirstOrDefaultAsync(x => x.Id == orderId);
        return order;
    }
}