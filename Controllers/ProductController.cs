using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Repositories;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;

    public ProductController(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("products")]
    public IActionResult GetProducts()
    {
        var products = _productRepository.GetAll();
        return Ok(products);
    }

    [HttpGet("products/{id:long}")]
    public async Task<IActionResult> GetProductsAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost("products")]
    public async Task<IActionResult> AddProductAsync()
    {
        return null;
    }
    
    [HttpPut("products/{id:long}")]
    public IActionResult UpdateProductById(long id)
    {
        return null;
    }
    
    [HttpDelete("products/{id:long}")]
    public IActionResult DeleteProductById(long id)
    {
        return null;
    }

}