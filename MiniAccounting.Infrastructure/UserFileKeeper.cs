namespace MiniAccounting.Infrastructure;

public class UserFileKeeper : IUserKeeper
{
    private ILogger _logger;
    private const string PATH_TO_FILE = "Users.json";

    public UserFileKeeper(ILogger logger)
    {
        _logger = new PrefixLogger(logger, $"[{nameof(UserFileKeeper)}] ");
    }

    public void Delete(Guid userUid)
    {
        _logger.Trace($"{nameof(Delete)}: {userUid}");
        var users = ReadUsers();
        var userToDelete = users.FirstOrDefault(x => x.Uid == userUid);
        if (userToDelete == null)
            throw new ArgumentException($"Юзер с uid '{userUid}' не найден.");

        users.Remove(userToDelete);
        WriteToFile(users);
    }

    public void Edit(User user)
    {
        _logger.Trace($"{nameof(Edit)}: {user}");
        var users = ReadUsers();
        var userToDelete = users.FirstOrDefault(x => x.Uid == user.Uid);

        if (userToDelete == null)
            throw new ArgumentException($"Юзер с uid '{user.Uid}' не найден.");

        users.Remove(userToDelete);
        users.Add(user);
        WriteToFile(users);
    }

    public User ReadUser(Guid userUid)
    {
        _logger.Trace($"{nameof(ReadUser)}: {userUid}");
        if (!File.Exists(PATH_TO_FILE))
            return null;

        var users = ReadUsers();
        return users.FirstOrDefault(x => x.Uid == userUid) 
            ?? throw new ArgumentException($"Юзер с uid '{userUid}' не найден."); ;
    }

    public List<User> ReadUsers()
    {
        _logger.Trace($"{nameof(ReadUsers)}");
        if (!File.Exists(PATH_TO_FILE))
            return new List<User>();

        using (var reader = new StreamReader(PATH_TO_FILE, Static.Encoding))
        {
            var text = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(text))
                return new List<User>();

            return JsonConvert.DeserializeObject<List<User>>(text);
        }
    }

    public void Save(User user)
    {
        _logger.Trace($"{nameof(Save)}: {user}");
        var users = ReadUsers();
        users.Add(user);
        WriteToFile(users);
    }

    public void SaveUsers(IEnumerable<User> users)
    {
        var usersString = String.Join(Environment.NewLine, users);
        _logger.Trace($"{nameof(SaveUsers)}:{Environment.NewLine} {usersString}");

        var newUsers = ReadUsers();
        newUsers.AddRange(users);
        WriteToFile(users);
    }

    private void WriteToFile(IEnumerable<User> users)
    {
        using (var writer = new StreamWriter(PATH_TO_FILE, false, Static.Encoding))
        {
            writer.Write(JsonConvert.SerializeObject(users));
        }
    }
}
