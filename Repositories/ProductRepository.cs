using System.Data;
using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    private static object _lock = new object();

    public bool TakeProduct(long productId, int takeQuantity)
    {
        using (var transaction = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            try
            {
                lock (_lock)
                {
                    var product = GetById(productId).Result;
                    var productQuantity = product.Quantity;

                    if (productQuantity - takeQuantity < 0)
                    {
                        return false;
                    }

                    product.Quantity -= takeQuantity;
                    SaveSync();
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}