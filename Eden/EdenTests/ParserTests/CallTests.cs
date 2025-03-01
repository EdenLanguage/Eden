using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class CallTests : FileTester
    {
        [Fact]
        public void SimpleCall()
        {
            string filename = "main7.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            BlockStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Statements.Length == 5);
        }
    }
}
