namespace MiniAccounting.Service.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OperatorController : ControllerBase
{
    private readonly Operator _operator;
    private readonly ILogger _logger;

    public OperatorController(ILogger logger, Operator @operator)
    {
        _operator = @operator;
        _logger = logger;
    }

    [HttpPut]
    public double TopUpTotalBalance(double addMoney, string comment)
    {
        return _operator.TopUpTotalBalance(addMoney, comment);
    }

    [HttpPut]
    public double RemoveFromTotalBalance(double removeMoney, string comment)
    {
        return _operator.RemoveFromTotalBalance(removeMoney, comment);
    }
}
