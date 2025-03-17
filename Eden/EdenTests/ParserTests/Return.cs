using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Parser;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Return : FileTester
    {
        [Fact]
        public void Valid()
        {
            string[][] data =
            [
                ["Return 50i;", "50"],
                ["Return False;", "False"],
                ["Return INt;", "INt"],
                ["Return zmienna;", "zmienna"],
                ["Return 50i*50i;", "(50*50)"],
                ["Return 0.1f+0.2f;", "(0.1+0.2)"],
            ];

            string[] testset = new string[] { };
            string input = string.Empty;
            string expected = string.Empty;
            string actual = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                testset = data[i];

                input = testset[0];
                expected = testset[1];

                Parser parser = new Parser();
                Statement ast = parser.Parse(input);

                if (ast is InvalidStatement asInvalidStmt)
                {
                    Assert.Fail(asInvalidStmt.Print());
                }

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);
                Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

                ReturnStatement vds = parser.Program.Block.Statements[0] as ReturnStatement;
            }
        }

        [Fact]
        public void Invalid()
        {
            string[][] data =
            [
                ["Return Bool;", null],
                ["Return IN5sd5;", null],
                ["Return zm6ienna;", null],
            ];

            string[] testset = new string[] { };
            string input = string.Empty;
            string expected = string.Empty;
            string actual = string.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                testset = data[i];

                input = testset[0];
                expected = testset[1];

                Parser parser = new Parser();
                Statement ast = parser.Parse(input);

                if (ast is not InvalidStatement asInvalidStmt)
                {
                    Assert.Fail($"'{input}' should fail but it didn't!");
                }
            }
        }

        [Fact]
        public void Return1()
        {
            string filename = "return1.eden";
            string executionLocation = GetParserSourceFile(filename);

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            if (ast is InvalidStatement asInvalidStatement)
            {
                Assert.Fail(asInvalidStatement.Print());
            }

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();
        }
    }
}
