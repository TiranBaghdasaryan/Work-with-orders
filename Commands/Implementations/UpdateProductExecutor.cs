using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class UpdateProductExecutor : IUpdateProductExecutor
{
    private UpdateProductRequestModel _parameter;
    private readonly IProductService _productService;
    
    public UpdateProductExecutor(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> Execute()
    {
        //to do [validation]

        return await _productService.UpdateProduct(_parameter);
    }

    public IUpdateProductExecutor WithParameter(UpdateProductRequestModel parameter)
    {
        _parameter = parameter;
        return this;
    }
}