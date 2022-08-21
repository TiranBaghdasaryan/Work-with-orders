using Work_with_orders.Entities;

namespace Work_with_orders.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> GetByEmailAsync(string email);
    public Task<bool> FillUpUserBalanceById(long id, decimal count);
    public Task<bool> BlockUserById(long id);
    public bool TakeBalanceById(long id, decimal count);
    public Task<bool> UnblockUserById(long id);
}