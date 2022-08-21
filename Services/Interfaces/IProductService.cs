using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;

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