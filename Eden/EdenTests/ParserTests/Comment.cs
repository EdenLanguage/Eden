using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
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
            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToASTFormat();
            string STR = block.ToString();
        }
    }
}