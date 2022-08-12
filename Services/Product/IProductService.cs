using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Commands.Executors;
using Work_with_orders.Models.ProductModels.CreateProduct;
using Work_with_orders.Models.ProductModels.ProductQuantity.AddProductQuantity;
using Work_with_orders.Models.ProductModels.ProductQuantity.RemoveProductQuantity;
using Work_with_orders.Models.ProductModels.UpdateProduct;
using Work_with_orders.Models.ProductModels.ViewModels;

namespace Work_with_orders.Services.Product;

public interface IProductService
{
    Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts();
    Task<ActionResult<ProductViewModel>> GetProductById( long id);
    Task<ActionResult<CreateProductResponseModel>> CreateProduct(CreateProductRequestModel request);
    Task<ActionResult<UpdateProductResponseModel>> UpdateProduct(UpdateProductRequestModel request);
    Task<IActionResult> DeleteProductById(long id);
    Task<ActionResult<AddProductQuantityResponseModel>> AddProductQuantity(AddProductQuantityRequestModel request);

    Task<ActionResult<RemoveProductQuantityResponseModel>> RemoveProductQuantity(
        RemoveProductQuantityRequestModel request);
}