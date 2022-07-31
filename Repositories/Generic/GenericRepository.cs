using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;

namespace Work_with_orders.Repositories.Generic;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _db;
    private readonly ApplicationContext _context;

    public GenericRepository(ApplicationContext applicationContext)
    {
        _context = applicationContext;
        _db = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(long id) => await _db.FirstOrDefaultAsync(x => Equals(x, id));
    public IEnumerable<TEntity> GetAll() => _db;
    public async Task AddAsync(TEntity entity) => await _db.AddAsync(entity);
    public void Update(TEntity entity) => _db.Update(entity);
    public void Delete(TEntity entity) => _db.Remove(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}