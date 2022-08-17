using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<Product?> GetById(long id);

    public IEnumerable<Product> GetAll();

    public Task Add(Product entity);

    public void Update(Product entity);

    public void Delete(Product entity);

    bool TakeProduct(long productId, int takeQuantity);

    public Task Save();
}