using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.RequestModels;
using Work_with_orders.Models.ResponseModels;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.Interfaces;
using Work_with_orders.Services.Interfaces;

namespace Work_with_orders.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;

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

    public async Task<IActionResult> BlockUserById(long id)
    {
        var isBlocked = await _userRepository.BlockUserById(id);

        if (isBlocked)
        {
            return new OkObjectResult("The user has successfully blocked.");
        }

        return new OkObjectResult("The user blocking has failed.");
    }

    public async Task<IActionResult> UnblockUserById(long id)
    {
        // to do
        var isBlocked = await _userRepository.UnblockUserById(id);

        if (isBlocked)
        {
            return new OkObjectResult("The user has successfully Unblocked.");
        }

        return new OkObjectResult("The user Unblocking has failed.");
    }
}