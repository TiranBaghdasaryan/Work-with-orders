using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Commands.Implementations;

public class DeleteProductExecutor : IDeleteProductExecutor
{
    private IProductService _productService;
    private long _parameter;

    public DeleteProductExecutor(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Execute()
    {
        // to do validation

        return await _productService.DeleteProductById(_parameter);
    }

    public IDeleteProductExecutor WithParameter(long parameter)
    {
        _parameter = parameter;
        return this;
    }
}