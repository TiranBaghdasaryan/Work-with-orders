using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Commands.Interfaces;

public interface ISignUpExecutor : ICommand
{
    public ISignUpExecutor WithParameter(SignUpRequestModel model);
}