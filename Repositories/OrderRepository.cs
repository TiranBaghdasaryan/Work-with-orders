using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

   
}