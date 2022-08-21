using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IOrderProductRepository
{
    Task AddProductInOrder(long orderId, long productId, int quantity);
    public Task<List<OrderProduct>> GetProductsByOrderId(long orderId);
    Task Save();
}