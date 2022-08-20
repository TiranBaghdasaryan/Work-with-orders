namespace Work_with_orders.Commands.Executors.AdminExecutor.UnblockUser;

public interface IUnblockUserExecutor : ICommand
{
    public IUnblockUserExecutor WithParameter(long id);

}