using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccounting.Infrastructure
{
    public class User
    {
        [Key]
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public double Money { get; set; }

        public User(string name, double money) : this (name, money, Guid.NewGuid())
        {
        }   

        public User(string name, double money, Guid uid)
        {
            Uid = uid;
            Name = name;
            Money = money;
        }

        public User()
        {
            
        }

        public override string ToString()
        {
            return Static.ToStringPropsWithReflection(this);
        }
    }
}