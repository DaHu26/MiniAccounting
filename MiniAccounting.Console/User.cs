using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole
{
    internal class User
    {
        public string Name;
        public int Wallet;
        public User(string name, int wallet)
        {
            Name = name;
            Wallet = wallet;
        }
    }
}
