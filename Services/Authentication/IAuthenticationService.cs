using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Services.Authentication;

public interface IAuthenticationService
{
    Task<ResultModel> SignUp(SignUpModel model);
    Task<ResultModel> SignIn(SignInModel model);
}