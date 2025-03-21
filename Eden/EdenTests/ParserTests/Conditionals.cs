using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
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
            AbstractSyntaxTreeNode block = parser.Parse(code);

            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            if(block is InvalidStatement)
            {
                Assert.Fail($"Statement: '{code}' failed!");
            }
        }

        [Fact]
        public void IfElseExpression()
        {
            string code = "If(True){Var Int zmienna = 5i;}Else{Var Int counter = 20i;};";

            Parser parser = new Parser();
            AbstractSyntaxTreeNode block = parser.Parse(code);

            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            if (block is InvalidStatement)
            {
                Assert.Fail($"Statement: '{code}' failed!");
            }
        }

        [Fact]
        public void IfElseExpressionFromFile()
        {
            string filename = "main4.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            AbstractSyntaxTreeNode block = parser.ParseFile(executionLocation);

            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            if (block is InvalidStatement)
            {
                Assert.Fail($"File: '{executionLocation}' failed!");
            }
        }
    }
}