using AutoMapper;
using Work_with_orders.Common;
using Work_with_orders.Context;
using Work_with_orders.Entities;
using Work_with_orders.Models.Authentication;
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


    public async Task<ResultModel> SignUp(SignUpModel model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email);
        
        if (!Equals(user, null))
        {
            return new ResultModel("User already exists.", 404);
        }

        user = new User();
        _mapper.Map(model, user);
        user.Password = user.Password.Hash();
        await _userRepository.Add(user);

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.Save();

        return new ResultModel(new TokenModel(accessToken, refreshToken));
    }

    public async Task<ResultModel> SignIn(SignInModel model)
    {
        User user = await _userRepository.GetByEmailAsync(model.Email);
        if (Equals(user, null))
        {
            return new ResultModel("The user doesn't exist.", 404);
        }

        if (!model.Password.Verify(user.Password)) return new ResultModel("The password is incorrect.", 404);

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userRepository.Save();

        return new ResultModel(new TokenModel(accessToken, refreshToken));
    }
}