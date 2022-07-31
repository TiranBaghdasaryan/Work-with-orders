using System.ComponentModel.DataAnnotations;

namespace Work_with_orders.Models.Authentication;

public class SignUpModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, ErrorMessage = "Must be less than 30 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(30, ErrorMessage = "Must be less than 30 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Number is required")]
    [StringLength(20, ErrorMessage = "Must be less than 20 characters")]
    public string PhoneNumber { get; set; }

    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid Email Address")]
    [Required]
    
    [StringLength(50, ErrorMessage = "Must be less than 50 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}