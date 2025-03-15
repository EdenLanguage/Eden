using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Conditionals : FileTester
    {
        [Fact]
        public void IfExpression()
        {
            string code = "If(True){Var Int zmienna = 5i;};";

            Parser parser = new Parser();

            FileStatement block = parser.Parse(code) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void IfElseExpression()
        {
            string code = "If(True){Var Int zmienna = 5i;}Else{Var Int counter = 20i;};";

            Parser parser = new Parser();

            FileStatement block = parser.Parse(code) as FileStatement;
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

    }
}
