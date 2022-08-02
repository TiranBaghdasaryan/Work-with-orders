namespace Work_with_orders.Models.Authentication;

public class TokenModel
{
    public TokenModel(string accessToken)
    {
        AccessToken = accessToken;
    }

    
    public string AccessToken { get; set; }
    //public string RefreshToken { get; set; }
    
    
}