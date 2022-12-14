using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Models.ResponseModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Route("v1/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync([FromServices] ISignUpExecutor executor, SignUpRequestModel model)
    {   
        return await executor.WithParameter(model).Execute();
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponseModel>> SignInAsync(SignInRequestModel model)
    {
        var response = await _authenticationService.SignIn(model);
        return response;
    }
}