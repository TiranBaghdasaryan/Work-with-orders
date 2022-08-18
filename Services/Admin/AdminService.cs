using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;
using Work_with_orders.Repositories;

namespace Work_with_orders.Services.Admin;

public class AdminService : IAdminService
{
    private IUserRepository _userRepository;

    public AdminService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> FillUpUserBalanceById(long id, FillUpUserBalanceRequest request)
    {
        var isFilled = await _userRepository.FillUpUserBalanceById(id, request.Count);

        var response = new FillUpUserBalanceResponse()
        {
            Message = "the balance filling has been successfully"
        };

        if (isFilled)
        {
            return new OkObjectResult(response);
        }

        response.Message = "The balance filling failed";

        return new BadRequestObjectResult(response);
    }

  
}