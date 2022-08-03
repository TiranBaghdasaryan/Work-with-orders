namespace Work_with_orders.Repositories.Generic;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(long id);
    IEnumerable<TEntity> GetAll();
    Task Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Save();
}