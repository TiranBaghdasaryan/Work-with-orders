using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Authentication;
using Work_with_orders.Services.Authentication;

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
    public async Task<IActionResult> SignUpAsync(SignUpModel model)
    {
        var result = await _authenticationService.SignUp(model);
        if (result.Code == 404)
        {
            return BadRequest(result.Message);
        }

        return Ok(result?.TokenModel);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInModel model)
    {
        var result = await _authenticationService.SignIn(model);
        if (result.Code == 404)
        {
            return BadRequest(result.Message);
        }

        return Ok(result?.TokenModel);
    }
}