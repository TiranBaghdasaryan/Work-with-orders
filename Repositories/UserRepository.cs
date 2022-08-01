using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return (await _db.FirstOrDefaultAsync(x => Equals(x.Email, email)))!;
    }

}