﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAccountingConsole.Logger
{
    public class ConsoleLogger : AbstractLogger
    {
        public static readonly ConsoleLogger Instance = new ConsoleLogger();

        protected override void PrivateWrite(string fullMsg)
        {
            Console.WriteLine(fullMsg);
        }
    }
}
