using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class SignUpExecutor : ISignUpExecutor
{
    private SignUpRequestModel _model;
    private readonly IAuthenticationService _authenticationService;

    public SignUpExecutor(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public ISignUpExecutor WithParameter(SignUpRequestModel model)
    {
        _model = model;
        return this;
    }
    
    public async Task<IActionResult> Execute()
    {
        return new OkObjectResult(await _authenticationService.SignUp(_model));
    }
}