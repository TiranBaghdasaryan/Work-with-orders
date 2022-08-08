using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class BasketRepository : GenericRepository<Basket>
{
    public BasketRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<Basket> GetBasketByUserId(long id)
    {
        var basket = await _db.FirstOrDefaultAsync(x => x.UserId == id);
        return basket;
    }
    
   
}