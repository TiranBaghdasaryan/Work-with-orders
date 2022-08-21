using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Models.ResponseModels;

namespace Work_with_orders.Services.Interfaces;

public interface IAuthenticationService
{
    Task<ActionResult<SignUpResponseModel>> SignUp(SignUpRequestModel model);
    Task<ActionResult<SignInResponseModel>> SignIn(SignInRequestModel model);
}