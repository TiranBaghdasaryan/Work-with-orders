using System.Data;
using Npgsql;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Options;
using Work_with_orders.Repositories.Generic;

namespace Work_with_orders.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext applicationContext) : base(applicationContext)
    {
    }

    // public bool TakeProduct(long productId, int takeQuantity)
    // {
    //     using (var transaction = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
    //     {
    //         try
    //         {
    //             var product = GetById(productId).Result;
    //
    //             var productQuantity = product.Quantity;
    //
    //             if (productQuantity - takeQuantity < 0)
    //             {
    //                 return false;
    //             }
    //
    //             product.Quantity -= takeQuantity;
    //             SaveSync();
    //             transaction.Commit();
    //             return true;
    //         }
    //         catch (Exception e)
    //         {
    //             Console.WriteLine(e);
    //             throw;
    //         }
    //     }
    // }

    public bool TakeProduct(long productId, int takeQuantity)
    {
        var connection = new NpgsqlConnection(ConnectionStrings.ConnectionPostgreSQL);
        connection.Open();
        string query =
            $"UPDATE  \"Products\" SET \"Quantity\" = \"Quantity\" - {takeQuantity} WHERE \"Id\" = {productId}";

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