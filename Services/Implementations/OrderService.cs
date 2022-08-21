using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Enums;
using Work_with_orders.Models.Order;
using Work_with_orders.Models.ProductModels.ViewModels;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;
using Work_with_orders.Repositories.OrderProductRepo;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IUserRepository _userRepository;
    private readonly BasketRepository _basketRepository;
    private readonly BasketProductRepository _basketProductRepository;
    private readonly OrderRepository _orderRepository;
    private readonly OrderProductRepository _orderProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;


    public OrderService
    (
        IUserRepository userRepository,
        BasketRepository basketRepository,
        BasketProductRepository basketProductRepository,
        OrderRepository orderRepository,
        OrderProductRepository orderProductRepository,
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
                bool isTaken = _productRepository.TakeProduct(item.ProductId, item.Quantity);
                var product = await _productRepository.GetById(item.ProductId);

                var productId = item.ProductId;
                var productName = product.Name;
                var productQuantity = item.Quantity;

                if (isTaken)
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


          


            using (var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    user = await _userRepository.GetByEmailAsync(email);
                    _context.Entry(user).State = EntityState.Modified;
                    
                    if (user.Balance >= order.Amount)
                    {
                        user.Balance -= order.Amount;
                        _basketProductRepository.RemoveAllProductsFromBasket(basket.Id);
                        await _userRepository.Save();
                        await transaction.CommitAsync();

                        return new OkObjectResult(responseModel);
                    }

                    order.Status = OrderStatus.Reject;
                    await _userRepository.Save();
                    await transaction.CommitAsync();
                    
                    return new BadRequestObjectResult("Your balance not enough to do this transaction");
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        return new BadRequestObjectResult("The basket is empty.");
    }
}