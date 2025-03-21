using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
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

            FileStatement output = parser.Parse(code) as FileStatement;

            string AST = output.ToAbstractSyntaxTree();
            string STR = output.ToString();
        }

        [Fact]
        public void GetArrayIndex()
        {
            string filename = "main24.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            Assert.True(parser.Errors.Length == 0);
        }
    }
}
