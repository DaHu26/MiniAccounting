using CommandLine;

namespace MiniAccounting.UI.Console
{
    public class CmdArgs
    {
        [Option(Default = 4)]
        public byte LogLevel { get; set; }
        [Option(Required = true)]
        public string MiniAccountingServiceAddress { get; set; }
    }
}
