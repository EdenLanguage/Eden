using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.AbstractSyntaxTreeTests
{
    public class Printing : ConsoleWriter
    {
        public Printing(ITestOutputHelper consoleWriter) : base(consoleWriter) { }

        [Fact]
        public void VariableDeclaration_1()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration_1)}");

            string input = "Var Float funcCall = 3.14 * zmienna / 2;";
            string output = "...";

            Parser parser = new Parser();
            FileStatement ast = parser.Parse(input);

            string toString = ast.ToString();
            string toAST = ast.ToASTFormat();

            Assert.Equal(parser.Program.Block.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void VariableDeclaration_2()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration_2)}");

            string input = "Var Int variable = 5 + 5 * 2 - 1 + 5;";
            string output = "...";

            Parser parser = new Parser();
            FileStatement ast = parser.Parse(input);

            string toString = ast.ToString();
            string toAST = ast.ToASTFormat();

            Assert.Equal(parser.Program.Block.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void VariableDeclaration_3()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration_3)}");

            string input = "Var String name = \"Maciek\";";
            string output = "...";

            Parser parser = new Parser();
            FileStatement ast = parser.Parse(input);

            string toString = ast.ToString();
            string toAST = ast.ToASTFormat();

            Assert.Equal(parser.Program.Block.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void ReturnStatement_1()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.ReturnStatement_1)}");

            string input = "Return 50*50;";
            string output = "...";

            Parser parser = new Parser();
            FileStatement ast = parser.Parse(input);

            string toString = ast.ToString();
            string toAST = ast.ToASTFormat();

            Assert.Equal(parser.Program.Block.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.Program.Block.Statements[0] as VariableDeclarationStatement;


            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }
    }
}