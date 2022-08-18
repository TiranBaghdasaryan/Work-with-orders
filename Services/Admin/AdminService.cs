﻿using Microsoft.AspNetCore.Mvc;
using Work_with_orders.Models.AdminModels;
using Work_with_orders.Repositories;

namespace Work_with_orders.Services.Admin;

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
        throw new NotImplementedException();
    }
}