using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Services.Interfaces;
using Work_with_orders.Validations;

namespace Work_with_orders.Commands.Implementations;

public class GetProductExecutor : IGetProductExecutor
{
    private long _parameter;
    private readonly IProductService _productService;
    private readonly CheckProductByIdValidation _validator;


    public GetProductExecutor(CheckProductByIdValidation validator, IProductService productService)
    {
        _productService = productService;
        _validator = validator;
    }

    public IGetProductExecutor WithParameter(long parameter)
    {
        _parameter = parameter;
        return this;
    }


    public async Task<IActionResult> Execute()
    {
        var result = _validator.Validate(_parameter, options => options.IncludeRuleSets("Manually"));
       
        if (!result.IsValid)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkObjectResult(await _productService.GetProductById(_parameter));
    }
}