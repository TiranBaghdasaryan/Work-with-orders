using FluentValidation;
using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Validations;

public class ProductCreateModelValidation : AbstractValidator<CreateProductRequestModel>
{
    private const int ProductNameMaximumLength = 50;

    public ProductCreateModelValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The product Name cannot be empty.")
            .MaximumLength(ProductNameMaximumLength)
            .WithMessage($"First Name must be less than {ProductNameMaximumLength} characters");

        RuleFor(x => x.Price).NotEmpty().WithMessage("The product price cannot be empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("The description cannot be empty.");
        
    }
}
