using Microsoft.AspNetCore.Mvc;

namespace Work_with_orders.Commands;

public abstract class Command<T> where T : class
{
    protected T validator;

    // inject validator
    public Command(T validator)
    {
        this.validator = validator;
    }

    protected abstract void Validation();

    protected abstract Task<IActionResult> ProcessExecution();

    public virtual Task<IActionResult> Execute()
    {
        Validation();
        return ProcessExecution();
    }
}