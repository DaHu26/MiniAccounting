namespace MiniAccounting.Infrastructure;

public interface IUserKeeper
{
    void Edit(User user);
    void Save(User user);
    void SaveUsers(IEnumerable<User> users);
    void Delete(Guid userUid);
    List<User> ReadUsers();
    User ReadUser(Guid userUid);
}
