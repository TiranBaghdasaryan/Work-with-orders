using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class CreateProductExecutor : ICreateProductExecutor
{
    private CreateProductRequestModel _parameter;
    private IProductService _productService;

    public CreateProductExecutor(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> ProcessExecution()
    {
        //to do [validation]
        
        return await _productService.CreateProduct(_parameter);
    }

    public ICreateProductExecutor WithParameter(CreateProductRequestModel parameter)
    {
        _parameter = parameter;
        return this;
    }
}