using System.Runtime.CompilerServices;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Models.Product;
using Work_with_orders.Repositories;
using IsolationLevel = System.Data.IsolationLevel;

namespace Work_with_orders.Controllers;

[ApiController]
[Route("v1/products")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;

    public ProductController(ProductRepository productRepository, IMapper mapper, ApplicationContext applicationContext,
        ApplicationContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
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
        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

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
    public async Task<IActionResult> UpdateProductById(long id, ProductUpdateModel productUpdateModel)
    {
        var product = await _productRepository.GetById(id);
        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

        _mapper.Map(productUpdateModel, product);
        _productRepository.Update(product);
        await _productRepository.Save();
        return Ok("The product updated successfully.");
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

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/quantity")]
    public async Task<IActionResult> AddProductQuantity(long id, [FromBody] int quantity)
    {
        var product = await _productRepository.GetById(id);
        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

        product.Quantity += quantity;
        await _productRepository.Save();
        return Ok("The product was added successfully.");
    }

    private static object _lock = new object();

    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}/quantity")]
    public async Task<IActionResult> RemoveProductQuantity(long id, [FromBody] int quantity)
    {
        await using (var transaction = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            try
            {
                // var product = await _productRepository.GetById(id);
                // if (Equals(product, null))
                // {
                //     return BadRequest("The product does not exist.");
                // }
                // product.Quantity -= quantity;
                // await _productRepository.Save();
                // await transaction.CommitAsync();

                Product product;
                lock (_lock)
                {
                    product = (_productRepository.GetById(id).Result)!;

                    if (Equals(product, null))
                    {
                        return BadRequest("The product does not exist.");
                    }

                    product.Quantity -= quantity;
                    _productRepository.SaveSync();
                    transaction.Commit();
                }

                // return Ok(product.Quantity);

                return Ok("The product was successfully deleted.");
            }
            catch (Exception exception)
            {
                await transaction.RollbackAsync();
                if (exception.InnerException != null && exception.InnerException.Message.Contains("CK_Quantity"))
                {
                    return BadRequest("So many products are not in warehouse.");
                }

                throw;
            }
        }
    }
}