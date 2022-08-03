using Microsoft.EntityFrameworkCore;
using Work_with_orders.Common;
using Work_with_orders.Entities;
using Work_with_orders.Enums;

namespace Work_with_orders.Context.Seeds;

public static class DefaultAdmin
{
    public static async Task Seed(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        ApplicationContext applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        try
        {
            var defaultUser = new User()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Address = "none",
                DateCreated = DateTime.UtcNow.Date,
                IsVerified = true,
                Password = "00_root_00".Hash(),
                PhoneNumber = "none",
                Role = Role.Admin,
                Email = "admin@gmail.com",
            };

            var user = await applicationContext.Users.FirstOrDefaultAsync(x => x.Email == defaultUser.Email);

            if (Equals(user, null))
            {
                await applicationContext.Users.AddAsync(defaultUser);
                await applicationContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}