namespace MiniAccounting.Infrastructure.DataKeepers
{
    public class UserDbKeeper : IUserKeeper
    {
        private readonly ILogger _logger;
        private readonly MiniAccountingContext _userDb;

        public UserDbKeeper(ILogger logger, MiniAccountingContext userDb)
        {
            _userDb = userDb;
            _logger = new PrefixLogger(logger, $"[{nameof(UserDbKeeper)}] ");
        }

        public void Delete(Guid userUid)
        {
            _logger.Trace($"{nameof(Delete)}: {userUid}");
            var userToDelete = _userDb.Users.FirstOrDefault(u => u.Uid == userUid);
            if (userToDelete == null)
                throw new ArgumentException($"Юзер с uid '{userUid}' не найден.");

            _userDb.Users.Remove(userToDelete);
            _userDb.SaveChanges();
        }

        public void Edit(User user)
        {
            _logger.Trace($"{nameof(Edit)}: {user}");
            var userToEdit = _userDb.Users.FirstOrDefault(user1 => user1.Uid == user.Uid);

            foreach (var prop in userToEdit.GetType().GetProperties())
            {
                prop.SetValue(userToEdit, prop.GetValue(user));
            }

            _userDb.SaveChanges();
        }

        public User ReadUser(Guid userUid)
        {
            _logger.Trace($"{nameof(ReadUser)}: {userUid}");
            var userToReturn = _userDb.Users.FirstOrDefault(user => user.Uid == userUid);
            if (userToReturn == null)
                throw new ArgumentException($"Юзер с uid '{userUid}' не найден.");

            return userToReturn;
        }

        public List<User> ReadUsers()
        {
            _logger.Trace($"{nameof(ReadUsers)}");
            return _userDb.Users.ToList();
        }

        public void Save(User user)
        {
            _logger.Trace($"{nameof(Save)}, {user.Name}");
            _userDb.Users.Add(user);
            _userDb.SaveChanges();
        }

        public void SaveUsers(IEnumerable<User> users)
        {
            var usersString = string.Join(Environment.NewLine, users);
            _logger.Trace($"{nameof(SaveUsers)}:{Environment.NewLine} {usersString}");

            _userDb.Users.AddRange(users);
            _userDb.SaveChanges();
        }
    }
}