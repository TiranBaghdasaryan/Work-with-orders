using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    public AuthenticationController()
    {
    }

    [HttpPost("sign-up")]
    public IActionResult SignUp(SignUpModel model)
    {
        
        return null;
    }

    [HttpPost("sign-in")]
    public IActionResult SignIp(SignInModel model)
    {
        
        return null;
    }
}