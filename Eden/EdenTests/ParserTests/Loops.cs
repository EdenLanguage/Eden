using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Loops : FileTester
    {
        [Fact]
        public void FileInput()
        {
            string filename = "main28.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToASTFormat();
            string STR = block.ToString();

            Assert.Equal(0, parser.Errors.Length);
        }
    }
}
