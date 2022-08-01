using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Generic;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase<long>
{
    protected readonly DbSet<TEntity> _db;
    protected readonly ApplicationContext _context;

    public GenericRepository(ApplicationContext applicationContext)
    {
        _context = applicationContext;
        _db = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return (await _db.FirstOrDefaultAsync(x => Equals(x.Id, id)))!;
    }

    public virtual IEnumerable<TEntity> GetAll() => _db;

    public virtual async Task AddAsync(TEntity entity) => await _db.AddAsync(entity);

    public virtual void Update(TEntity entity) => _db.Update(entity);
    public virtual void Delete(TEntity entity) => _db.Remove(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}