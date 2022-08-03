using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Models.Product;
using Work_with_orders.Repositories;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ProductController(ProductRepository productRepository, IMapper mapper, ApplicationContext applicationContext)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = applicationContext;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productRepository.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProducts(long id)
    {
        var product = await _productRepository.GetById(id);
        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductCreateModel productCreateModel)
    {
        var product = new Product();
        _mapper.Map(productCreateModel, product);

            await _productRepository.Add(product);
            await _productRepository.Save();

        return Ok("The product was created successfully.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public IActionResult UpdateProductById(long id, ProductUpdateModel productUpdateModel)
    {
        return null;
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductById(long id)
    {
        var product = await _productRepository.GetById(id);
        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

        _productRepository.Delete(product);
        await _productRepository.Save();
        return Ok("The product was successfully deleted.");
    }
}