﻿namespace Work_with_orders.Models.Authentication;

public class TokenModel
{
    public TokenModel(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    
    
}