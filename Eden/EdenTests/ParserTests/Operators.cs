using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Operators : FileTester
    {
        [Fact]
        public void Files()
        {
            string filename = "operators1.eden";
            string executionLocation = GetParserSourceFile(filename);

            Parser parser = new Parser();

            AbstractSyntaxTreeNode block = parser.ParseFile(executionLocation);
            
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            if(block is InvalidStatement)
            {
                Assert.Fail($"File: '{executionLocation}' failed but it shouldn't");
            }
        }
    }
}
