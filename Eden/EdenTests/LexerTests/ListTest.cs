using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class List : FileTester
    {
        [Fact]
        public void List_Of_Int()
        {
            string filename = "main22.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();
        }
    }
}
