using System.Diagnostics;

namespace MiniAccountingConsole.Logger
{
    public sealed class DebugLogger : AbstractLogger
    {
        public static readonly DebugLogger Instance = new DebugLogger();

        protected override void PrivateWrite(string fullMsg)
        {
            Debug.WriteLine(fullMsg);
        }
    }
}
