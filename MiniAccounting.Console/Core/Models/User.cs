using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole.Core
{
    public class User
    {
        public Guid Uid { get; private set; }
        public string Name { get; private set; }
        public double Money { get; private set; }

        public User(string name, double money) : this (name, money, Guid.NewGuid())
        {
        }

        public User(string name, double money, Guid uid)
        {
            Uid = uid;
            Name = name;
            Money = money;
        }

        public override string ToString()
        {
            return Static.ToStringPropsWithReflection(this);
        }
    }
}