using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IBasketProductRepository
{
    Task Save();
    public Task AddProductInBasket(long basketId, long productId, int quantity);
    public Task<bool> RemoveProductFromBasket(long basketId, long productId);
    public bool RemoveAllProductsFromBasket(long basketId);
    public Task<List<BasketProduct>> GetAllProductsInBasketByBasketId(long basketId);
}