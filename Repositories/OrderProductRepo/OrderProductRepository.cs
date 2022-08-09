using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.OrderProductRepo;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly ApplicationContext _context;

    public OrderProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddProductInOrder(long orderId, long productId, int quantity)
    {
        await _context.OrderProduct.AddAsync(new Entities.OrderProduct()
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
        });
    }

    public async Task<List<OrderProduct>> GetProductsByOrderId(long orderId)
    {
        var productsInOrder = await _context.OrderProduct.Where(x => x.OrderId == orderId).ToListAsync();
        return productsInOrder;
    }

    public async Task Save() => await _context.SaveChangesAsync();
}