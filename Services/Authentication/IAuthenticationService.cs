using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Services.Authentication;

public interface IAuthenticationService
{
    TokenModel SignUp(SignUpModel model);
    TokenModel SignIn(SignInModel model);
    TokenModel RenewToken(TokenModel request);
}