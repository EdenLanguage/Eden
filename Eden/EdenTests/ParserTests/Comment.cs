using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Comment : FileTester
    {
        [Fact]
        public void FromFile_1()
        {
            string filename = "main27.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;

            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();
        }
    }
}