using FluentValidation;
using Work_with_orders.Common;
using Work_with_orders.Models.Authentication;
using Work_with_orders.Repositories;

namespace Work_with_orders.Validations.Authentication;

public class SignInModelValidation : AbstractValidator<SignInModel>
{
    private readonly UserRepository _userRepository;

    public SignInModelValidation(UserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Must be a valid Email Address.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.");

        RuleFor(x => x).Must(CheckUserCredentials).WithMessage("The email or password is incorrect.");

        bool CheckUserCredentials(SignInModel model)
        {
            var user = _userRepository.GetByEmailAsync(model.Email).Result;
            
            if (Equals(user, null) || !model.Password.Verify(user.Password))
            {
                return false;
            }

            return true;
        }
    }
}