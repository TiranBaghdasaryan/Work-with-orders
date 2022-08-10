﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;
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
    public Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
    {
        var response = _productService.GetProducts();
        return response;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> GetProduct(long id)
    {
        var response = await _productService.GetProductById(id);
        return response;
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel request)
    {
        var response = await _productService.CreateProduct(request);
        return response;
    }


    [HttpPut("product")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UpdateProductResponseModel>> UpdateProduct(UpdateProductRequestModel request)
    {
        var response = await _productService.UpdateProduct(request);
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
    [HttpPost("product/quantity")]
    public async Task<ActionResult<AddProductQuantityResponseModel>> AddProductQuantity(
        AddProductQuantityRequestModel request)
    {
        var response = await _productService.AddProductQuantity(request);
        return response;
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("product/quantity")]
    public async Task<ActionResult<RemoveProductQuantityResponseModel>> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request)
    {
        var response = await _productService.RemoveProductQuantity(request);
        return response;
    }
}