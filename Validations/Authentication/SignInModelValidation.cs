using FluentValidation;
using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Validations.Authentication;

public class SignInModelValidation : AbstractValidator<SignInModel>
{
    public SignInModelValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Must be a valid Email Address.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.");
    }
}