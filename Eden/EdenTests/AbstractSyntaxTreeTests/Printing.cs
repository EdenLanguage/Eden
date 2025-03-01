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
        public void VariableDeclaration1()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration1)}");

            string input = "Var Float funcCall = 3.14 * zmienna / 2;";
            string output = "...";

            Parser parser = new Parser();
            BlockStatement ast = parser.Parse(input);

            Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.AbstractSyntaxTree.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.AbstractSyntaxTree.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void VariableDeclaration2()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration2)}");

            string input = "Var Int variable = 5 + 5 * 2 - 1 + 5;";
            string output = "...";

            Parser parser = new Parser();
            BlockStatement ast = parser.Parse(input);

            Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.AbstractSyntaxTree.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.AbstractSyntaxTree.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void VariableDeclaration3()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.VariableDeclaration3)}");

            string input = "Var String name = \"Maciek\";";
            string output = "...";

            Parser parser = new Parser();
            BlockStatement ast = parser.Parse(input);

            Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.AbstractSyntaxTree.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.AbstractSyntaxTree.Statements[0] as VariableDeclarationStatement;

            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }

        [Fact]
        public void ReturnStatement1()
        {
            PrintTestName($"{nameof(Printing)}.{nameof(Printing.ReturnStatement1)}");

            string input = "Return 50*50;";
            string output = "...";

            Parser parser = new Parser();
            BlockStatement ast = parser.Parse(input);

            Assert.Equal(parser.AbstractSyntaxTree.Statements.Length, 1);
            Assert.Equal(parser.Errors.Length, 0);
            Assert.True(parser.AbstractSyntaxTree.Statements[0] is not InvalidStatement);

            VariableDeclarationStatement vds = parser.AbstractSyntaxTree.Statements[0] as VariableDeclarationStatement;


            Console.WriteLine($"Input data: {input}");
            Console.WriteLine("Output data:");
            Console.WriteLine($"{ast.ToASTFormat()}");
            Console.WriteLine("|------------------------------------|\n");
        }
    }
}
