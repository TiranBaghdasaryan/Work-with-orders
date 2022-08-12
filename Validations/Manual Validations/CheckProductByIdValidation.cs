using FluentValidation;
using Work_with_orders.Repositories;

namespace Work_with_orders.Validations.Manual_Validations;

public class CheckProductByIdValidation : AbstractValidator<long>
{
    private readonly ProductRepository _productRepository;

    public CheckProductByIdValidation(ProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x)
            .Must(ProductExists)
            .WithMessage("The product does not exist. [From Validator]");
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