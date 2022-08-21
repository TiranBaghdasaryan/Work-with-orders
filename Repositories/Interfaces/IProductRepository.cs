using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    bool TakeProduct(long productId, int takeQuantity);
}