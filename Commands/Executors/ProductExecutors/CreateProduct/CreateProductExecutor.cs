using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Services.Product;

namespace Work_with_orders.Commands.Executors.ProductExecutors.CreateProduct;

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