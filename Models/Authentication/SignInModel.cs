using System.ComponentModel.DataAnnotations;

namespace Work_with_orders.Models.Authentication;

public class SignInModel
{
    [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid Email Address")]
    [Required]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}