using Work_with_orders.Models.AdminModels;

namespace Work_with_orders.Commands.Executors.AdminExecutor;

public interface IFillUpUserBalanceExecutor : ICommand
{
    public IFillUpUserBalanceExecutor WithParameter(long id, FillUpUserBalanceRequest parameter);
}