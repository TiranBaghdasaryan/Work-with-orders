using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Repositories.BasketProduct;

namespace Work_with_orders.Repositories.BasketProductRepo;

public class BasketProductRepository : IBasketProductRepository
{
    private readonly ApplicationContext _context;

    public BasketProductRepository(ApplicationContext applicationContext)
    {
        _context = applicationContext;
    }

    public async Task AddProductInBasket(long basketId, long productId, int quantity)
    {
        await _context.BasketProduct.AddAsync(new Entities.BasketProduct()
        {
            BasketId = basketId,
            ProductId = productId,
            Quantity = quantity,
        });
    }


    public async Task<bool> RemoveProductFromBasket(long basketId, long productId)
    {
        var basketProduct =
            await _context.BasketProduct.FirstOrDefaultAsync(x => x.BasketId == basketId && x.ProductId == productId);
        if (Equals(basketProduct, null))
        {
            return false;
        }

        _context.BasketProduct.Remove(basketProduct);
        return true;
    }

    public bool RemoveAllProductsFromBasket(long basketId)
    {
        var basketProduct = _context.BasketProduct.Where(x => x.BasketId == basketId);
        if (Equals(basketProduct, null))
        {
            return false;
        }

        _context.BasketProduct.RemoveRange(basketProduct);
        return true;
    }
    
    public async  Task<IEnumerable<Entities.BasketProduct>> GetAllProductsInBasket(long basketId)
    {
        var basketProduct = await _context.BasketProduct.Where(x => x.BasketId == basketId).ToListAsync();
        return basketProduct;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}