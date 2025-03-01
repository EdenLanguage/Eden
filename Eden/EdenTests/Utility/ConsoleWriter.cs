using Pastel;
using System.Drawing;
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

        public void PrintTestName(string testname)
        {
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine(testname.Pastel(Color.Green));
            Console.WriteLine("|------------------------------------|\n");
        }
    }
}
