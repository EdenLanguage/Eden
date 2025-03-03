using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class FunctionTests : FileTester
    {
        [Fact]
        public void Function_1()
        {
            string filename = "main5.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void Function_2()
        {
            string filename = "main6.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }
    }
}