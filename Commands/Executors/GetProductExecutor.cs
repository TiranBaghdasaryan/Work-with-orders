using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Services.Product;
using Work_with_orders.Validations.Manual_Validations;

namespace Work_with_orders.Commands.Executors;

public class GetProductExecutor : Command<CheckProductByIdValidation>
{
    private long _parameter;
    private readonly IProductService _productService;

    public GetProductExecutor(CheckProductByIdValidation validator, IProductService productService) : base(validator)
    {
        _productService = productService;
        this.validator = validator;
    }

    public Command<CheckProductByIdValidation> WithParameter(long parameter)
    {
        _parameter = parameter;
        return this;
    }

    private ValidationResult result;

    protected override void Validation()
    {
        result = validator.Validate(_parameter, options => options.IncludeRuleSets("Manually"));
    }

    protected override async Task<IActionResult> ProcessExecution()
    {
        if (!result.IsValid)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkObjectResult(await _productService.GetProductById(_parameter));
    }
}