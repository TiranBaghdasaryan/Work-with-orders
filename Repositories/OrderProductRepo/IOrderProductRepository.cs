namespace Work_with_orders.Repositories.OrderProductRepo;

public interface IOrderProductRepository
{
    Task AddProductInOrder(long orderId,long productId,int quantity);
    Task Save();
}