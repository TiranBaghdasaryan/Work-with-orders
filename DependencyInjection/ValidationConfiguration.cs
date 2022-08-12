﻿using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Work_with_orders.Validations.Authentication;
using Work_with_orders.Validations.Manual_Validations;
using Work_with_orders.Validations.Product;

namespace Work_with_orders.DependencyInjection;

public static class ValidationConfiguration
{
    public static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;
            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssemblyContaining<SignInModelValidation>();
        services.AddValidatorsFromAssemblyContaining<SignUpModelValidation>();
        services.AddValidatorsFromAssemblyContaining<ProductCreateModelValidation>();
        //services.AddValidatorsFromAssemblyContaining<CheckProductByIdValidation>();

        services.AddScoped<IValidator<long>, CheckProductByIdValidation>();

        return services;
    }
}