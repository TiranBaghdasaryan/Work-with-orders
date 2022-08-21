namespace Work_with_orders.Models.RequestModels;

public class RefreshTokenRequestModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}