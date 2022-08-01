using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Enums;
using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationContext _context;

    public AuthenticationService(ApplicationContext context)
    {
        _context = context;
    }

    public TokenModel SignUp(SignUpModel model)
    {
        throw new NotImplementedException();
    }

    public TokenModel SignIn(SignInModel model)
    {
        throw new NotImplementedException();
    }

    public TokenModel RenewToken(TokenModel request)
    {
        throw new NotImplementedException();
    }


}