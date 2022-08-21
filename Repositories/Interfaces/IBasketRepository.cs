using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IBasketRepository : IGenericRepository<Basket>
{
    public Task<Basket> GetBasketByUserId(long id);
}