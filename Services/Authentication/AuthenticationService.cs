using Microsoft.EntityFrameworkCore;
using Work_with_orders.Common;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<User> _users;

    public AuthenticationService(ApplicationContext context)
    {
        _context = context;
        _users = _context.Users;
    }

    public async Task<ResultModel> SignUpAsync(SignUpModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultModel> SignInAsync(SignInModel model)
    {
        User user = (await _users.FirstOrDefaultAsync(u => Equals(u.Email, model.Email)))!;
        if (Equals(user,null)) return new ResultModel("The user doesn't exist.",404);
        if (!model.Password.Verify(user.Password)) return new ResultModel("The password is incorrect.",404);
        
        // verified return token [to-do]

        return new ResultModel(new TokenModel(null,null));
    }

    public ResultModel RenewToken(TokenModel request)
    {
        throw new NotImplementedException();
    }
    
    
    
    
    
}