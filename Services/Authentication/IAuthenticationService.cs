using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Services.Authentication;

public interface IAuthenticationService
{
    Task<ResultModel> SignUpAsync(SignUpModel model);
    Task<ResultModel> SignInAsync(SignInModel model);
}