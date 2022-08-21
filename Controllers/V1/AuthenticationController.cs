using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;
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
    public async Task<ActionResult<SignUpResponseModel>> SignUpAsync(SignUpRequestModel model) 
    {
        var response = await _authenticationService.SignUp(model);
        return response;
    }

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponseModel>> SignInAsync(SignInRequestModel model)
    {
        var response = await _authenticationService.SignIn(model);
        return response;
    }
}