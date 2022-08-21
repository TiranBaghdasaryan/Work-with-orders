using FluentValidation;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Repositories;

namespace Work_with_orders.Validations.Authentication;

public class SignUpModelValidation : AbstractValidator<SignUpRequestModel>
{
    private const int FirstNameMaximumLength = 30;
    private const int LastNameMaximumLength = 30;
    private const int AddressMaximumLength = 50;
    private const int PhoneNumberMaximumLength = 20;
    private const int PasswordMaximumLength = 255;

    private const int PasswordMinimumLength = 255;

    private readonly IUserRepository _userRepository;

    public SignUpModelValidation(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name cannot be empty.")
            .MaximumLength(FirstNameMaximumLength)
            .WithMessage($"First Name must be less than {FirstNameMaximumLength} characters");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name cannot be empty.")
            .MaximumLength(LastNameMaximumLength)
            .WithMessage($"Last Name must be less than {LastNameMaximumLength} characters");

        RuleFor(x => x.Address).NotEmpty().WithMessage("The address cannot be empty.")
            .MaximumLength(AddressMaximumLength)
            .WithMessage($"Address must be less than {AddressMaximumLength} characters");

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The phone number cannot be empty.")
            .MaximumLength(PhoneNumberMaximumLength)
            .WithMessage($"The phone number must be less than {PhoneNumberMaximumLength} characters")
            .Matches(@"^\+374-\d{2}-\d{2}-\d{2}-\d{2}$").
            WithMessage("The phone number format needs to be like +374-99-63-10-71");
            
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .EmailAddress().WithMessage("Must be a valid Email Address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password cannot be empty.")
            .MaximumLength(PasswordMaximumLength)
            .WithMessage($"Password must be less than {PhoneNumberMaximumLength} characters")
            .MaximumLength(PasswordMinimumLength)
            .WithMessage($"Password must be more than {PasswordMinimumLength} characters");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("The confirm password cannot be empty.")
            .Equal(x => x.Password).WithMessage("The confirm password does not match the password.");

        RuleFor(x => x).Must(CheckUserExists).WithMessage("That email is already in use by the other user.");
        
        bool CheckUserExists(SignUpRequestModel model)
        {
            var user = _userRepository.GetByEmailAsync(model.Email).Result;
            
            if (Equals(user,null))
            {
                return true;
            }

            return false;
        }
    }
}