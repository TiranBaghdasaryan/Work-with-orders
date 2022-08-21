namespace Work_with_orders.Commands.Interfaces;

public interface IUnblockUserExecutor : ICommand
{
    public IUnblockUserExecutor WithParameter(long id);

}