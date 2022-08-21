﻿using Microsoft.AspNetCore.Authentication;
using Work_with_orders.Services.Implementations;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.DependencyInjection;

public static class ServiceConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ITokenService, TokenService>();

    }
}