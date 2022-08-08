namespace Work_with_orders.Repositories.BasketProduct;

public interface IBasketProductRepository
{
    Task AddProductInBasket(long basketId,long productId,int quantity);
    Task Save();
}