using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    internal class UserFileKeeper : IUserKeeper
    {
        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }

        public User ReadUser(Guid uid)
        {
            throw new NotImplementedException();
        }

        public User[] ReadUsers()
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }
    }
}
