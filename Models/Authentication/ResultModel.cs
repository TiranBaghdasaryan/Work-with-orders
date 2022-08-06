namespace Work_with_orders.Models.Authentication;

public class ResultModel
{
    public string Message { get; set; }
    public int Code { get; set; }
    public TokenModel TokenModel { get; set; }
    
    public ResultModel(string message, int code)
    {
        Message = message;
        Code = code;
    }
    public ResultModel(TokenModel tokenModel)
    {
        Message = "Token received successfully.";
        Code = 200;
        TokenModel = tokenModel;
    }

}