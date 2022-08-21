using Work_with_orders.Models.RequestModels;

namespace Work_with_orders.Commands.Interfaces;

public interface IFillUpUserBalanceExecutor : ICommand
{
    public IFillUpUserBalanceExecutor WithParameter(long id, FillUpUserBalanceRequest parameter);
}