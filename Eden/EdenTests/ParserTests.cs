using EdenClasslibrary.Parser;
using EdenClasslibrary.Parser.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenTests
{
    public class ParserTests
    {
        [Fact]
        public void ParserTest_InitialTest()
        {
            string code = "var int zmienna = 10;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_ThreeStatements()
        {
            string code = 
                "var int zmienna = 10;" +
                "var int counter = 0;" +
                "var int minuser = 20;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 3);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_InvalidStatement_1()
        {
            string code =
                "var zmienna = 10;" +
                "int counter = 0;" +
                "var int minuser = ;" +
                "var int counter = 120;";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 3);
        }

        [Fact]
        public void ParserTest_InvalidStatement_2()
        {
            string code =
                "var int zmienna = 10;" +
                "int counter = 0";

            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 1);
        }

        [Fact]
        public void ParserTest_BoolStatement_1()
        {
            string code =
                "var bool flag = false;";
            Parser parser = new Parser();
            parser.Parse(code);

            //string json = parser.Statements.Serialize();

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_Return_1()
        {
            string code =
                "return 5;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_Expression_1()
        {
            string code = "counter;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
        }

        [Fact]
        public void ParserTest_Expression_2()
        {
            string code = "5;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);

            ExpressionStatement expressionStmnt = (parser.AST.Statements[0] as ExpressionStatement);
            Assert.NotNull(expressionStmnt);

            IntLiteral intLit = (expressionStmnt.Expression as IntLiteral);
            Assert.NotNull(intLit);

            Assert.Equal(intLit.Value, 5);
        }

        [Fact]
        public void ParserTest_Expression_3()
        {
            string code = "1+2+3;";
            Parser parser = new Parser();
            parser.Parse(code);

            Assert.Equal(parser.AST.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);

            ExpressionStatement expressionStmnt = (parser.AST.Statements[0] as ExpressionStatement);
            Assert.NotNull(expressionStmnt);

            IntLiteral intLit = (expressionStmnt.Expression as IntLiteral);
            Assert.NotNull(intLit);

            Assert.Equal(intLit.Value, 5);
        }
    }
}
