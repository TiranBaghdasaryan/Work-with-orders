using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    bool TakeProduct(long productId, int takeQuantity);
}