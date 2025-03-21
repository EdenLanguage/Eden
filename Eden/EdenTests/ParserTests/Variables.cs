using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class Variables : ConsoleWriter
    {
        public Variables(ITestOutputHelper consoleWriter) : base(consoleWriter) { }

        [Fact]
        public void CorrectStatements()
        {
            const int testCount = 6;

            string[] codes = new string[testCount]
            {
                "Var Int counter = 10i;",
                "Var Bool flaga = True;",
                "Var String name = \"Pratt\";",
                "Var Float pi = 3.14f;",
                "Var Int sum = counter + 5i - licznik * 10i;",
                "Var Float funcCall = 3.14f * zmienna / 2i;",
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
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);
                Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

                VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;
                
                // ID
                string actualID = vds.Identifier.Name;
                Log.WriteLine($"{expectedID} <- ExpectedID");
                Log.WriteLine($"{actualID} <- ActualID");
                Assert.Equal(actualID, expectedID);

                // Expression
                Log.WriteLine($"{expectedExpression} <- ExpectedExp");
            }
        }

        [Fact]
        public void InvalidStatements()
        {
            const int testCount = 6;

            string[] codes =
            [
                "Var Intcounter = 10i;",
                "Var Bool 4flaga = True;",
                "Var Strig name = \"Pratt\";",
                "var Float pi = 3.14f;",
                "Var Int sum = counter5 - licznik * 10i;",
                "Var Float funcCall = 3.14f * zmienna / 2i",
            ];

            string code = string.Empty;
            string expectedID = string.Empty;
            string expectedExpression = string.Empty;

            for (int i = 0; i < testCount; i++)
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
        public void ComplesStatements()
        {
            const int testCount = 1;

            string[] inputCodes = new string[testCount]
            {
                "Var Int counter = (5i*10i)/2i+2i*(1i/2i);",
            };

            string[][] expectedOutputs = new string[][]
            {
                new string[]{ "Var", "IntObject", "counter", "(((5*10)/2)+(2*(1/2)))"},
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string[] expected = new string[] { };

            for (int i = 0; i < testCount; i++)
            {
                code = inputCodes[i];
                expected = expectedOutputs[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code) as FileStatement;

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);
                Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

                VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;

                // Type
                VariableTypeExpression type = vds.Type;
                Assert.NotNull(type);
                Assert.Equal(type.Type.Name, expected[1]);
                
                // Identifier
                IdentifierExpression ie = vds.Identifier;
                Assert.NotNull(ie);
                Assert.Equal(ie.Name, expected[2]);

                // Expression
                EdenClasslibrary.Types.AbstractSyntaxTree.Expressions.Expression exp = vds.Expression;
                Assert.NotNull(exp);
                Assert.Equal(exp.ToString(), expected[3]);

            }
        }
    }
}
