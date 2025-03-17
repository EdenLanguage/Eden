using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class Expression : ConsoleWriter
    {
        public Expression(ITestOutputHelper consoleWriter) : base(consoleWriter)
        {
        }

        [Fact]
        public void Identifier()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "zmienna;",
                "counter;",
                "counter + 2i;",
                "- counter + 10i;",
                "counter + counter + zmienna - zmienna;",
            };

            string[] expecteds = new string[testCount]
            {
                "zmienna",
                "counter",
                "(counter+2)",
                "((-counter)+10)",
                "(((counter+counter)+zmienna)-zmienna)",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                Log.WriteLine($"{expected} <- Expected");

            }
        }

        [Fact]
        public void VariableType()
        {
            string[] codes =
            [
                "Int;",
                "Float;",
                "String;",
            ];

            string code = string.Empty;

            for (int i = 0; i < codes.Length; i++)
            {
                code = codes[i];

                Parser parser = new Parser();
                Statement ast = parser.Parse(code);

                if (ast is not InvalidStatement asInvalidStmt)
                {
                    Assert.Fail($"'{code}' should fail but it didn't!");
                }
            }
        }

        [Fact]
        public void Binary()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "1i+2i+3i;",
                "1i*2i+3i;",
                "1i+2i*3i;",
                "1i/2i*3i;",
                "-1i*2i/3i;",
            };

            string[] expecteds = new string[testCount]
            {
                "((1+2)+3)",
                "((1*2)+3)",
                "(1+(2*3))",
                "((1/2)*3)",
                "(-((1*2)/3))",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);


                Log.WriteLine($"{expected} <- Expected");

            }
        }

        [Fact]
        public void Unary()
        {
            const int testCount = 10;

            string[] codes = new string[testCount]
            {
                "-1i;",
                "+1i;",
                "-52i;",
                "-00001i;",
                "-1424i;",
                "-3.14f;",
                "-0.123123f;",
                "+1.5235235f;",
                "0i;",
                "325i;",
            };

            string[] expecteds = new string[testCount]
            {
                "(-1)",
                "1",
                "(-52)",
                "(-1)",
                "(-1424)",
                "(-3.14)",
                "(-0.123123)",
                "1.5235235",
                "0",
                "325",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for(int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                Log.WriteLine($"{expected} <- Expected");
            }
        }

        [Fact]
        public void Grouped()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "(5i+5i)*2i;",
                "5i+5i*2i;",
                "5i==(5i==True);",
                "5i==5i==True;",
                "(1i+2i)*(3i+4i);",
            };

            string[] expecteds = new string[testCount]
            {
                "((5+5)*2);",
                "(5+(5*2));",
                "(5==(5==True));",
                "((5==5)==True);",
                "((1+2)*(3+4));",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                string actual = expressionStmnt.ToString();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }
    }
}
