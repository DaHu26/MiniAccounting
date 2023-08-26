using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    public interface IUserKeeper
    {
        void Edit(User user);
        void Save(User user);
        void SaveUsers(IEnumerable<User> users);
        void Delete(Guid userUid);
        List<User> ReadUsers();
        User ReadUser(Guid userUid);
    }
}
