using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ErrorTests
{
    public class FileErrorTests : FileTester
    {
        [Fact]
        public void InvalidStatements()
        {
            string[] data = new string[]
            {
                "Var int counter = 10;",
                "Var Float counter = 3,145;",
                "Var Bool flaga = evaluateFlag(10)",
                "Var int pajacyk = 5i;",
                "var String imie = \"5553\";",
                "Var Stringasdasd = \"Pawel\";",
                "Var Int variable = 10i",
                "Var String surname = \"sdasd\"",
            };

            foreach (string input in data)
            {
                Parser parser = new Parser();

                FileStatement output = parser.Parse(input);

                Assert.True(parser.Errors.Length != 0);

                string errors = parser.PrintErrors();
            }
        }

        [Fact]
        public void FileErrors()
        {
            string filename = "main8.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement output = parser.ParseFile(executionLocation);

            string toAST = output.ToASTFormat();
            string toString = output.ToString();
            string errors = parser.PrintErrors();

            Assert.True(toAST != null);
        }
    }
}