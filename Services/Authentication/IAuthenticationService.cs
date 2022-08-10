using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;

namespace Work_with_orders.Services.Authentication;

public interface IAuthenticationService
{
    Task<SignUpResponseModel> SignUp(SignUpRequestModel model);
    Task<SignInResponseModel> SignIn(SignInRequestModel model);
}