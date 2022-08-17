using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Services.Product;
using Work_with_orders.Validations.Manual_Validations;

namespace Work_with_orders.Commands.Executors;

public class GetProductExecutor : IGetProductExecutor, ICommand<CheckProductByIdValidation>
{
    private long _parameter;
    private readonly IProductService _productService;
    private ValidationResult result;

        
    //
    public GetProductExecutor(CheckProductByIdValidation validator, IProductService productService)
    {
        _productService = productService;
        Validator = validator;
    }
    
    public ICommand<CheckProductByIdValidation> WithParameter(long parameter)
    {
        _parameter = parameter;
        return this;
    }

    public CheckProductByIdValidation Validator { get; set; }

    public void Validation()
    {
        result = Validator.Validate(_parameter, options => options.IncludeRuleSets("Manually"));
    }
    
    public async Task<IActionResult> ProcessExecution()
    {
        if (!result.IsValid)
        {
            return new BadRequestObjectResult(result.Errors);
        }
        
        return new OkObjectResult(await _productService.GetProductById(_parameter));
    }

    public async Task<IActionResult> Execute()
    {
        Validation();
        return await ProcessExecution();
    }
}