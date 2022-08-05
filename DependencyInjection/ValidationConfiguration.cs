using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Work_with_orders.Validations.Authentication;

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
        return services;
    }
}