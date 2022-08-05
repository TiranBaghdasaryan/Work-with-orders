using FluentValidation;
using Work_with_orders.Models.Authentication;

namespace Work_with_orders.Validations.Authentication;

public class SignUpModelValidation : AbstractValidator<SignUpModel>
{
    private const int FirstNameMaximumLength = 30;
    private const int LastNameMaximumLength = 30;
    private const int AddressMaximumLength = 50;
    private const int PhoneNumberMaximumLength = 20;
    private const int PasswordMaximumLength = 255;
    
    private const int PasswordMinimumLength = 255;
    
    public SignUpModelValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name cannot be empty.")
            .MaximumLength(FirstNameMaximumLength).WithMessage($"First Name must be less than {FirstNameMaximumLength} characters");
        
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name cannot be empty.")
            .MaximumLength(LastNameMaximumLength).WithMessage($"Last Name must be less than {LastNameMaximumLength} characters");

        RuleFor(x => x.Address).NotEmpty().WithMessage("The address cannot be empty.")
            .MaximumLength(AddressMaximumLength).WithMessage($"Address must be less than {AddressMaximumLength} characters");
        
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The phone number cannot be empty.")
            .MaximumLength(PhoneNumberMaximumLength).WithMessage($"The phone number must be less than {PhoneNumberMaximumLength} characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email cannot be empty.")
            .EmailAddress().WithMessage("Must be a valid Email Address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("The password cannot be empty.")
            .MaximumLength(PasswordMaximumLength).WithMessage($"Password must be less than {PhoneNumberMaximumLength} characters")
            .MaximumLength(PasswordMinimumLength).WithMessage($"Password must be more than {PasswordMinimumLength} characters");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("The confirm password cannot be empty.")
            .Equal(x => x.Password).WithMessage("The confirm password does not match the password.");



    }
}