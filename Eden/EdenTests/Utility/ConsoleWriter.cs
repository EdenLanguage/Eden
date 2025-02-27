using Xunit.Abstractions;

namespace EdenTests.Utility
{
    public class ConsoleWriter
    {
        public readonly ITestOutputHelper Log;
        public ConsoleWriter(ITestOutputHelper consoleWriter)
        {
            Log = consoleWriter;
        }
    }
}
