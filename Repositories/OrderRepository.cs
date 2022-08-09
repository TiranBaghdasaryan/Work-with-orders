using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<List<Order>> GetOrdersByUserId(long userId)
    {
        var orders = await _db.Where(x => x.UserId == userId).ToListAsync();
        return orders;
    }


}