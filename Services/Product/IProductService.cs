using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;

namespace Work_with_orders.Services.Product;

public interface IProductService
{
    Task<IActionResult> GetProducts();
    Task<ActionResult<ProductViewModel>> GetProductById(long id);
    Task<IActionResult> CreateProduct(CreateProductRequestModel request);
    Task<IActionResult> UpdateProduct(UpdateProductRequestModel request);
    Task<IActionResult> DeleteProductById(long id);
    Task<IActionResult> AddProductQuantity(AddProductQuantityRequestModel request);

    Task<ActionResult<RemoveProductQuantityResponseModel>> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request);
}