using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Work_with_orders.Enums;
using Work_with_orders.Repositories;

namespace Work_with_orders.Filters;

public class CheckUserActivityFilter : IActionFilter
{
    private IUserRepository _userRepository;


    public void OnActionExecuting(ActionExecutingContext context)
    {
        _userRepository = (context.HttpContext.RequestServices.GetService(typeof(IUserRepository)) as IUserRepository)!;
        var email = context.HttpContext.User.FindFirstValue(ClaimTypes.Email);

        if (email is null)
        {
            return;
        }

        var user = _userRepository.GetByEmailAsync(email).GetAwaiter().GetResult();

        if (user.State == UserState.Blocked)
        {
            context.Result = new UnauthorizedObjectResult("You are blocked");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}