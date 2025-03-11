using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Indexer : FileTester
    {
        [Fact]
        public void IndexExpression()
        {
            string code = "array[0i];";

            Parser parser = new Parser();

            FileStatement output = parser.Parse(code);

            string AST = output.ToASTFormat();
            string STR = output.ToString();
        }

        [Fact]
        public void GetArrayIndex()
        {
            string filename = "main24.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            Assert.True(parser.Errors.Length == 0);
        }
    }
}
