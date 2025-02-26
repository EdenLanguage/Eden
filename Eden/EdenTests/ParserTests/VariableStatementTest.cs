using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class VariableStatementTest : ConsoleWriter
    {
        public VariableStatementTest(ITestOutputHelper consoleWriter) : base(consoleWriter) { }

        [Fact]
        public void CorrectStatements()
        {
            const int testCount = 6;

            string[] codes = new string[testCount]
            {
                "Var Int counter = 10;",
                "Var Bool flaga = True;",
                "Var String name = \"Pratt\";",
                "Var Float pi = 3.14;",
                "Var Int sum = counter + 5 - licznik * 10;",
                "Var Float funcCall = 3.14 * zmienna / 2;",
            };

            string[] expectedIDs = new string[testCount]
            {
                "counter",
                "flaga",
                "name",
                "pi",
                "sum",
                "funcCall",
            };

            string[] expectedExpressions = new string[testCount]
            {
                "10",
                "True",
                "Pratt",
                "3.14",
                "((counter+5)-(licznik*10))",
                "((3.14*zmienna)/2)",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expectedID = string.Empty;
            string expectedExpression = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expectedID = expectedIDs[i];
                expectedExpression = expectedExpressions[i];

                parser = new Parser();
                BlockStatement ast = parser.Parse(code);

                Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);
                Assert.True(parser.AbstractSyntaxTree.Statements[0] is not InvalidStatement);

                VariableDeclarationStatement vds = parser.AbstractSyntaxTree.Statements[0] as VariableDeclarationStatement;
                
                // ID
                string actualID = vds.Identifier.Name;
                Log.WriteLine($"{expectedID} <- ExpectedID");
                Log.WriteLine($"{actualID} <- ActualID");
                Assert.Equal(actualID, expectedID);

                // Expression
                string actualExp = vds.Expression.ParenthesesPrint();
                Log.WriteLine($"{expectedExpression} <- ExpectedExp");
                Log.WriteLine($"{actualExp} <- ActualExp");
                Assert.Equal(actualExp, expectedExpression);
            }
        }

        [Fact]
        public void InvalidStatements()
        {
            const int testCount = 6;

            string[] codes = new string[testCount]
            {
                "Var Intcounter = 10;",
                "Var Bool 4flaga = True;",
                "Var Strig name = \"Pratt\";",
                "var Float pi = 3.14;",
                "Var Int sum = counter5 - licznik * 10;",
                "Var Float funcCall = 3.14 * zmienna / 2",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expectedID = string.Empty;
            string expectedExpression = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];

                parser = new Parser();
                BlockStatement ast = parser.Parse(code);

                Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 1);
                Assert.True(parser.AbstractSyntaxTree.Statements[0] is InvalidStatement);
            }
        }
    }
}
