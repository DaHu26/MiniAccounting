using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new MainMenu();
            menu.Start();
        }
    }
}

