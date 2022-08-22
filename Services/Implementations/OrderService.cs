using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Enums;
using Work_with_orders.Models.ResponseModels;
using Work_with_orders.Repositories.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IUserRepository _userRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IBasketProductRepository _basketProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;


    public OrderService
    (
        IUserRepository userRepository,
        IBasketRepository basketRepository,
        IBasketProductRepository basketProductRepository,
        IOrderRepository orderRepository,
        IOrderProductRepository orderProductRepository,
        IProductRepository productRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _basketRepository = basketRepository;
        _basketProductRepository = basketProductRepository;
        _orderRepository = orderRepository;
        _orderProductRepository = orderProductRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrdersByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var orders = await _orderRepository.GetOrdersByUserId(user.Id);
        var responseModel = new List<OrderViewModel>(orders.Count);

        foreach (var order in orders)
        {
            var orderViewModel = new OrderViewModel();
            _mapper.Map(order, orderViewModel);
            responseModel.Add(orderViewModel);
        }

        return responseModel;
    }

    public async Task<ActionResult<OrderDetailsViewModel>> GetOrderDetails(long id, string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        var order = await _orderRepository.GetOrderById(id);

        if (Equals(order, null) || !Equals(order.UserId, user.Id) && user.Role != Role.Admin)
        {
            return new StatusCodeResult(401);
        }

        var productsInOrderViewModels = new List<ProductInOrderViewModel>();

        var productsInOrder = await _orderProductRepository.GetProductsByOrderId(order.Id);

        foreach (var orderProduct in productsInOrder)
        {
            var product = await _productRepository.GetById(orderProduct.ProductId);
            var productId = product.Id;
            var productName = product.Name;
            var productQuantity = orderProduct.Quantity;

            productsInOrderViewModels.Add(new ProductInOrderViewModel()
            {
                ProductId = productId,
                ProductName = productName,
                ProductQuantity = productQuantity,
            });
        }

        var orderDetails = new OrderDetailsViewModel()
        {
            Id = order.Id,
            DoneDate = order.DoneDate,
            Status = order.Status.ToString(),
            ProductsViewModels = productsInOrderViewModels,
        };

        return orderDetails;
    }

    public async Task<IActionResult> CreateOrderByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var basket = await _basketRepository.GetBasketByUserId(user.Id);

        if (!Equals(basket, null))
        {
            var productsInBasket = await _basketProductRepository.GetAllProductsInBasketByBasketId(basket.Id);
            var productsCountInBasket = productsInBasket.Count;

            if (productsCountInBasket < 1)
            {
                return new BadRequestObjectResult("The basket is empty.");
            }

            var order = new Entities.Order()
            {
                UserId = user.Id,
            };

            await _orderRepository.Add(order);
            await _orderRepository.Save();

            var responseModel = new CreateOrderResponseModel();

            foreach (var item in productsInBasket)
            {
                bool isProductTaken = _productRepository.TakeProduct(item.ProductId, item.Quantity);
                var product = await _productRepository.GetById(item.ProductId);

                var productId = item.ProductId;
                var productName = product.Name;
                var productQuantity = item.Quantity;

                if (isProductTaken)
                {
                    await _orderProductRepository.AddProductInOrder(order.Id, item.ProductId, item.Quantity);

                    responseModel.ProductsOrderedSuccessfully.Add(new ProductInOrderViewModel()
                    {
                        ProductId = productId,
                        ProductName = productName,
                        ProductQuantity = productQuantity
                    });

                    order.Amount += product.Price * productQuantity;
                }
                else
                {
                    responseModel.ProductsOrderedFailed.Add(new ProductInOrderViewModel()
                    {
                        ProductId = productId,
                        ProductName = productName,
                        ProductQuantity = productQuantity
                    });
                }
            }


            var isBalanceTaken = _userRepository.TakeBalanceById(user.Id, order.Amount);

            if (isBalanceTaken)
            {
                return new OkObjectResult(responseModel);
            }

            return new BadRequestObjectResult("Your balance not enough to do this transaction");

        }

        return new BadRequestObjectResult("The basket is empty.");
    }
}