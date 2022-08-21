using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Services.Interfaces;

public interface IProductService
{
    Task<IActionResult> GetProducts();
    Task<IActionResult> GetProductById(long id);
    Task<IActionResult> CreateProduct(CreateProductRequestModel request);
    Task<IActionResult> UpdateProduct(UpdateProductRequestModel request);
    Task<IActionResult> DeleteProductById(long id);
    Task<IActionResult> AddProductQuantity(AddProductQuantityRequestModel request);
    Task<IActionResult> RemoveProductQuantity(RemoveProductQuantityRequestModel request);
}