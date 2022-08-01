using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }
}