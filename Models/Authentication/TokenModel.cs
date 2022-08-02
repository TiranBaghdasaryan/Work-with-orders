using System.ComponentModel.DataAnnotations;

namespace Work_with_orders.Models.Authentication;

public class TokenModel
{
    public TokenModel(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    [Required] public string AccessToken { get; set; }
    [Required] public string RefreshToken { get; set; }
}