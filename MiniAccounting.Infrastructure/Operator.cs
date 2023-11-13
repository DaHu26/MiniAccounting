using MiniAccounting.Infrastructure.DataKeepers;
using MiniAccounting.Infrastructure.Logger;

namespace MiniAccounting.Infrastructure;

public class Operator
{
    public double TotalMoney { get; private set; }

    private ILogger _logger;
    private IReadWriteHistoryOfTransactions _readWriteHistoryOfTransactions;
    private IUserKeeper _userKeeper;

    public Operator(ILogger logger, IUserKeeper userKeeper, IReadWriteHistoryOfTransactions readWriteHistoryOfTransactions)
    {
        _logger = new PrefixLogger(logger, $"[{nameof(Operator)}] ");
        _readWriteHistoryOfTransactions = readWriteHistoryOfTransactions;
        _userKeeper = userKeeper;
    }

    public double TopUpTotalBalance(double addMoney, string comment)
    {
        _logger.WriteLine($"{nameof(TopUpTotalBalance)}: addmoney={addMoney}, comment={comment}");
        var operationInfo = new TransactionInfo(Guid.NewGuid(), DateTimeOffset.UtcNow, TypeOfTransaction.TopUp, comment, Guid.Empty, Guid.Empty);
        _readWriteHistoryOfTransactions.WriteTransaction(operationInfo);

        return TotalMoney += addMoney;
    }

    public double RemoveFromTotalBalance(double removeMoney, string comment)
    {
        _logger.WriteLine($"{nameof(RemoveFromTotalBalance)}: removeMoney={removeMoney}, comment={comment}");
        var operationInfo = new TransactionInfo(Guid.NewGuid(), DateTimeOffset.UtcNow, TypeOfTransaction.Remove, comment, Guid.Empty, Guid.Empty);
        _readWriteHistoryOfTransactions.WriteTransaction(operationInfo);

        return TotalMoney -= removeMoney;
    }

    public void SaveUser(User user)
    {
        _logger.WriteLine($"{nameof(SaveUser)} Name:{user.Name} Money:{user.Money}");
        _userKeeper.Save(user);
    }
    
    public void SaveUsers(IEnumerable<User> users)
    {
        var usersString = String.Join(Environment.NewLine, users);
        _logger.WriteLine($"{nameof(SaveUsers)}:{Environment.NewLine} {usersString}");
        _userKeeper.SaveUsers(users);
    }

    public List<User> ReadUsers()
    {
        _logger.Debug($"{nameof(ReadUsers)}");
        return _userKeeper.ReadUsers();
    }

    public User ReadUser(Guid userUid)
    {
        _logger.Debug($"{nameof(ReadUsers)} Id: {userUid}");
        return _userKeeper.ReadUser(userUid);
    }

    public void Edit(User user)
    {
        _logger.WriteLine($"{nameof(Edit)}: {user}");
        _userKeeper.Edit(user);
    }

    public void Delete(Guid userUid)
    {
        _logger.WriteLine($"{nameof(Delete)}: {userUid}");
        _userKeeper.Delete(userUid);
    }

    public double GetTotalBalance()
    {
        return TotalMoney;
    }
}
