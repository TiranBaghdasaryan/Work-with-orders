using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Enums;
using Work_with_orders.Options;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return (await _db.FirstOrDefaultAsync(x => Equals(x.Email, email)))!;
    }


    public async Task<bool> FillUpUserBalanceById(long id, decimal count)
    {
        // to do

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var user = await GetById(id);

                user.Balance += count;
                await Save();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(e);

                return false;
            }
        }
    }

    public async Task<bool> BlockUserById(long id)
    {
        try
        {
            var user = await GetById(id);
            user.State = UserState.Blocked;
            await Save();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return false;
        }
    }

    public bool TakeBalanceById(long id, decimal takeCount)
    {
        using (var connection = new NpgsqlConnection(ConnectionStrings.ConnectionPostgreSQL))
        {
            connection.Open();
            string query =
                $"UPDATE  \"Users\" SET \"Balance\" = \"Balance\" - {takeCount} WHERE \"Id\" = {id}";

            using (var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                using (var command = new NpgsqlCommand(query, connection, transaction))
                {
                    command.CommandTimeout = 120;

                    try
                    {
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        return true;

                        //  Console.WriteLine($"{Thread.CurrentThread.Name} did commit");
                    }
                    catch (NpgsqlException)
                    {
                        //   Console.WriteLine($"{Thread.CurrentThread.Name} did rollback");

                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}