using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Interfaces;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Services.Product;

namespace Work_with_orders.Controllers.V1;

[ApiController]
[Route("v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productService.GetProducts();
        return response;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct([FromServices] IGetProductExecutor executor, long id)
    {
        var response = await executor.WithParameter(id).Execute();
        return response;
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromServices] ICreateProductExecutor executor,
        CreateProductRequestModel request)
    {
        var response = await executor.WithParameter(request).Execute();
        return response;
    }


    [HttpPut("product")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct([FromServices] IUpdateProductExecutor executor,
        UpdateProductRequestModel request)
    {
        var response = await executor.WithParameter(request).Execute();
        return response;
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var response = await _productService.DeleteProductById(id);
        return response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("product/add")]
    public async Task<IActionResult> AddProductQuantity(
        AddProductQuantityRequestModel request)
    {
        var response = await _productService.AddProductQuantity(request);
        return response;
    }

    // [Authorize(Roles = "Admin")]
    [HttpPut("product/remove")]
    public async Task<IActionResult> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request)
    {
        var response = await _productService.RemoveProductQuantity(request);
        return response;
    }
}