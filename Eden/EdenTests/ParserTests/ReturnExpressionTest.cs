using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class ReturnExpressionTest : ConsoleWriter
    {
        public ReturnExpressionTest(ITestOutputHelper consoleWriter) : base(consoleWriter) { }

        [Fact]
        public void Valid()
        {
            string[] codes = new string[]
            {
                "Return 50;",
                "Return False;",
                "Return Bool;",
                "Return INt;",
                "Return IN5sd5",
                "Return zmienna;",
                "Return zm6ienna;",
                "Return 50*50;",
                "Return 0.1+0.2;",
            };

            string[] expecteds = new string[]
            {
                "50",
                "False",
                null,
                "INt",
                null,
                "zmienna",
                null,
                "(50*50)",
                "(0.1+0.2)",
            };

            bool equal = codes.Length == expecteds.Length;
            Assert.True(equal);

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < codes.Length; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                if(expected == null)
                {
                    //  Invalid
                    Assert.Equal(parser.Program.Block.Statements.Length, 1);
                    Assert.Equal(parser.Errors.Length, 1);
                    Assert.True(parser.Program.Block.Statements[0] is InvalidStatement);
                }
                else
                {
                    //  Valid
                    Assert.Equal(parser.Program.Block.Statements.Length, 1);
                    Assert.Equal(parser.Errors.Length, 0);
                    Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

                    ReturnStatement vds = parser.Program.Block.Statements[0] as ReturnStatement;

                    string actual = vds.Expression.ParenthesesPrint();
                    Log.WriteLine($"{expected} <- Expected");
                    Log.WriteLine($"{actual} <- Actual");
                    Assert.Equal(actual, expected);
                }
            }
        }
    }
}
