using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class IfExpressionTests : FileTester
    {
        [Fact]
        public void IfExpression()
        {
            string code = "If(True){Var Int zmienna = 5;};";

            Parser parser = new Parser();

            FileStatement block = parser.Parse(code);
            string AST = block.ToASTFormat();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void IfElseExpression()
        {
            string code = "If(True){Var Int zmienna = 5;}Else{Var Int counter = 20;};";

            Parser parser = new Parser();

            FileStatement block = parser.Parse(code);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void IfElseExpressionFromFile()
        {
            string filename = "main4.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

    }
}
