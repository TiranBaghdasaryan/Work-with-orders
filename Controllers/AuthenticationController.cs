﻿using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.Authentication;
using Work_with_orders.Services.Authentication;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpModel model)
    {
        ResultModel result = await _authenticationService.SignUpAsync(model);
        if (result.Code == 404) return BadRequest(result.Message);
        
        return Ok(result?.TokenModel);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInModel model)
    {
        ResultModel result = await _authenticationService.SignInAsync(model);
        if (result.Code == 404) return BadRequest(result.Message);
        return Ok(result?.TokenModel);
    }
}