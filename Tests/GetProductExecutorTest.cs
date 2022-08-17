// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Work_with_orders.Commands.Executors;
// using Work_with_orders.Services.Product;
// using Work_with_orders.Validations.Manual_Validations;
// using Xunit;
//
//
// namespace Tests;
//
// public class GetProductExecutorTest
// {
//     private GetProductExecutor _getProductExecutor;
//     private Mock<CheckProductByIdValidation> _mockCheckProductByIdValidation = new Mock<CheckProductByIdValidation>();
//     private Mock<IProductService> _mockIProductService = new Mock<IProductService>();
//
//
//     [Fact]
//     void WithParameterShouldInitParameter()
//     {
//         
//          //  var MyAssembly = Assembly.LoadFile(@"
//         
//         //arrange 
//         int id = 6;
//         _getProductExecutor = new GetProductExecutor(_mockCheckProductByIdValidation.Object,_mockIProductService.Object);
//         
//         
//         
//         //act
//         var result = _getProductExecutor.WithParameter(id);
//         
//         //assert
//         Assert.IsType<OkObjectResult>(result);
//     }
//
// }