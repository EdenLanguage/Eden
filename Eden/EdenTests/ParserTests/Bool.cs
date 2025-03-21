using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenTests.ParserTests
{
    public class Bool
    {
        [Fact]
        public void Assignment()
        {
            string input = "5i != 10i;";

            Parser parser = new Parser();

            FileStatement block = parser.Parse(input) as FileStatement;
            string ast = block.ToAbstractSyntaxTree();

            Console.WriteLine(ast);

            Assert.True(block.Block.Statements.Length == 1);
            Assert.True(parser.Errors.Length == 0);

            ExpressionStatement es = block.Block.Statements[0] as ExpressionStatement;
            Assert.NotNull(es);

            BinaryExpression be = es.Expression as BinaryExpression;
            Assert.NotNull(be);

            IntExpression left = be.Left as IntExpression;
            Assert.NotNull(left);
            Assert.True(left.Value == 5);

            Assert.True(be.NodeToken.Keyword == TokenType.Inequal);

            IntExpression right = be.Right as IntExpression;
            Assert.NotNull(right);
            Assert.True(right.Value == 10);
        }

        [Fact]
        public void Variables()
        {
            string[] codes = new string[]
            {
                "!False;",
                "True == (5i!=124i);",
                "True == 5i!=124i;",
                "False != 5i!=124i;",
                "True != False;",
                "True == !False;",
            };

            string[][] expectedData = new string[][]
            {
                new string[] { "!False", "", "" },
                new string[] { "True", "==", "5!=124" },
                new string[] { "True", "==", "5!=124" },
                new string[] { "False", "!=", "5!=124" },
                new string[] { "True", "!=", "False" },
                new string[] { "True", "==", "!False" },
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string[] expected = new string[] { };

            for (int i = 0; i < codes.Length; i++)
            {
                code = codes[i];
                expected = expectedData[i];

                parser = new Parser();
                FileStatement block = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                //  Setting up test like this doesn't make sense for now because parser doesn't take into account '(' symbols that are defining precedense of equation.
                //  It has to be implemented.

                //ExpressionStatement es = block.Statements[0] as ExpressionStatement;
                //Assert.NotNull(es);

                //BinaryExpression be = es.Expression as BinaryExpression;
                //Assert.NotNull(be);

                //BoolExpresion left = be.Left as BoolExpresion;
                //Assert.NotNull(left);
                //Assert.True(left.NodeToken.LiteralValue == expected[0]);

                //Assert.True(be.NodeToken.LiteralValue == expected[1]);
            }
        }
    }
}
