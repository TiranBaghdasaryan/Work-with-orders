﻿using AutoMapper;
using Work_with_orders.Common;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Enums;
using Work_with_orders.Models.AuthenticationModels.SignIn;
using Work_with_orders.Models.AuthenticationModels.SignUp;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Token;

namespace Work_with_orders.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationContext _context;

    private readonly UserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    #region Constructor

    public AuthenticationService
    (
        ApplicationContext context,
        UserRepository userRepository,
        OrderRepository orderRepository,
        ITokenService tokenService,
        IMapper mapper
    )
    {
        _context = context;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    #endregion


    public async Task<SignUpResponseModel> SignUp(SignUpRequestModel model)
    {
        var user = new User();
        _mapper.Map(model, user);
        user.Password = user.Password.Hash();
        user.Role = Role.User;
        await _userRepository.Add(user);

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.Save();

        var response = new SignUpResponseModel()
        {
            Tokens = new Dictionary<string, string>()
            {
                { "accessToken", accessToken },
                { "refreshToken", refreshToken }
            }
        };

        return response;
    }

    public async Task<SignInResponseModel> SignIn(SignInRequestModel model)
    {
        User user = await _userRepository.GetByEmailAsync(model.Email);

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.Save();

        var response = new SignInResponseModel()
        {
            Tokens = new Dictionary<string, string>()
            {
                { "accessToken", accessToken },
                { "refreshToken", refreshToken }
            }
        };

        return response;
    }
}