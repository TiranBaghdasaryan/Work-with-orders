using Microsoft.AspNetCore.Mvc;
using Moq;
using Work_with_orders.Commands.Executors;
using Work_with_orders.Controllers.V1;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Services.Product;
using Xunit;

namespace Tests;

public class ProductControllerTests
{
    private ProductController _productController;
    private Mock<IProductService> _productServiceMock = new Mock<IProductService>();
    private Mock<IGetProductExecutor> _getProductExecutor = new Mock<IGetProductExecutor>();

    ProductViewModel[] productViewModels = new ProductViewModel[]
    {
        new ProductViewModel()
        {
            Id = 3,
        },
        new ProductViewModel()
        {
            Id = 5,
        }
    };

    [Fact]
    public async Task GetProductShouldReturns_OkObjectResult()
    {
        //arrange 
        _productController = new ProductController(_productServiceMock.Object);


        long id = 5;

        _getProductExecutor.Setup(ex => ex.WithParameter(id).Execute())
            .ReturnsAsync(await GetIActionResult(id, productViewModels));

        //act
        var result = await _productController.GetProduct(_getProductExecutor.Object, id);

        //assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductShouldReturns_BadRequestObjectResult()
    {
        //arrange 
        _productController = new ProductController(_productServiceMock.Object);

        long id = 15;
        
        _getProductExecutor.Setup(ex => ex.WithParameter(id).Execute())
            .ReturnsAsync(await GetIActionResult(id, productViewModels));
        
        //act
        var result = await _productController.GetProduct(_getProductExecutor.Object, id);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    private async Task<IActionResult> GetIActionResult(long id, ProductViewModel[] productViewModels)
    {
        if (productViewModels.Any(x => x.Id == id))
        {
            return new OkObjectResult("");
        }

        return new BadRequestObjectResult("");
    }
}