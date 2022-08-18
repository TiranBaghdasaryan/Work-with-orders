using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Services.Product;

namespace Work_with_orders.Commands.Executors;

public class UpdateProductExecutor : IUpdateProductExecutor
{
    private UpdateProductRequestModel _parameter;
    private readonly IProductService _productService;

    public UpdateProductExecutor(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> ProcessExecution()
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