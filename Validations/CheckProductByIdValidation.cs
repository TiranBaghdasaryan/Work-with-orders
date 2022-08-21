using FluentValidation;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.Interfaces;

namespace Work_with_orders.Validations;

public class CheckProductByIdValidation : AbstractValidator<long>
{
    private readonly IProductRepository _productRepository;

    public CheckProductByIdValidation(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleSet("Manually", () =>
        {
            RuleFor(x => x)
                .Must(ProductExists)
                .WithMessage("The product does not exist. [From Validator]");
        });
    }


    private bool ProductExists(long id)
    {
        var product = _productRepository.GetById(id).Result;

        if (Equals(product, null))
        {
            return false;
        }

        return true;
    }
}