using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Repositories;
using IsolationLevel = System.Data.IsolationLevel;

namespace Work_with_orders.Controllers.V1;

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
    public async Task<IActionResult> GetProduct(long id)
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
    public async Task<ActionResult<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel request)
    {
        var product = new Product();
        _mapper.Map(request, product);

        await _productRepository.Add(product);
        await _productRepository.Save();

        var response = new CreateProductResponseModel()
        {
            Message = "The product was created successfully.",
        };

        return response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("product")]
    public async Task<ActionResult<UpdateProductResponseModel>> UpdateProduct(UpdateProductRequestModel request)
    {
        var product = await _productRepository.GetById(request.Id);

        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

        _mapper.Map(request, product);
        _productRepository.Update(product);
        await _productRepository.Save();

        var response = new UpdateProductResponseModel()
        {
            Message = "The product updated successfully."
        };

        return response;
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
    [HttpPost("product/quantity")]
    public async Task<ActionResult<AddProductQuantityResponseModel>> AddProductQuantity(
        AddProductQuantityRequestModel request)
    {
        var product = await _productRepository.GetById(request.Id);
        
        if (Equals(product, null))
        {
            return BadRequest("The product does not exist.");
        }

        product.Quantity += request.Quantity;
        await _productRepository.Save();

        return Ok("The product was added successfully.");
    }

    private static object _lock = new object();

    [Authorize(Roles = "Admin")]
    [HttpDelete("product")]
    public async Task<ActionResult<RemoveProductQuantityResponseModel>> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request)
    {
        await using (var transaction = _context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            try
            {
                lock (_lock)
                {
                    var product = (_productRepository.GetById(request.Id).Result)!;

                    if (Equals(product, null))
                    {
                        return BadRequest("The product does not exist.");
                    }

                    product.Quantity -= request.Quantity;
                    _productRepository.SaveSync();
                    transaction.Commit();
                }

                var response = new RemoveProductQuantityResponseModel()
                {
                    Message = "The product was successfully deleted."
                };

                return response;
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