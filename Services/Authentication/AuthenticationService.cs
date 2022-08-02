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
    private readonly OrderRepository _orderRepository;
    private readonly ITokenService _tokenService;

    #region Constructor

    public AuthenticationService
    (
        ApplicationContext context,
        UserRepository userRepository,
        OrderRepository orderRepository,
        ITokenService tokenService)
    {
        _context = context;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _tokenService = tokenService;
    }

    #endregion


    public async Task<ResultModel> SignUpAsync(SignUpModel model)
    {
        User user = await _userRepository.GetByEmailAsync(model.Email);
        if (!Equals(user, null)) return new ResultModel("User already exists.", 404);

        #region SignUpModel to User Manual mapping

        user = new User
        (
            model.FirstName,
            model.LastName,
            model.Address,
            model.PhoneNumber,
            model.Email,
            model.Password
        );

        #endregion

        #region Transaction

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            Order order = new Order(user.Id);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return new ResultModel("The transaction failed.", 404);
        }

        #endregion

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _userRepository.SaveChangesAsync();

        return new ResultModel(new TokenModel(accessToken, refreshToken));
    }

    public async Task<ResultModel> SignInAsync(SignInModel model)
    {
        User user = await _userRepository.GetByEmailAsync(model.Email);
        if (Equals(user, null)) return new ResultModel("The user doesn't exist.", 404);
        if (!model.Password.Verify(user.Password)) return new ResultModel("The password is incorrect.", 404);

        var claims = _tokenService.SetClaims(user.Email, user.Role);

        string accessToken = _tokenService.GenerateAccessToken(claims);
        string refreshToken = _tokenService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _userRepository.SaveChangesAsync();
        
        return new ResultModel(new TokenModel(accessToken, refreshToken));
    }
}