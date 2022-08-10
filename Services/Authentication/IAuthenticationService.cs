using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;

namespace Work_with_orders.Services.Authentication;

public interface IAuthenticationService
{
    Task<ActionResult<SignUpResponseModel>> SignUp(SignUpRequestModel model);
    Task<ActionResult<SignInResponseModel>> SignIn(SignInRequestModel model);
}