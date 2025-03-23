using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class List : FileTester
    {
        [Fact]
        public void Declaration()
        {
            string[] code = new string[]
            {
                "List Int primes = [2i,3i,5i,7i,9i];",
                "List Float primes = [2i, 1.5f, \"John\"];",
                "List Float pies = [1.1f,2.2f,3.3f,4.4f,5.5f];",
                "List String names = [ \"Mark\" ,\"Adam\",\"Jordan\",\"David\",\"Isaac\"];",
                "List Int temp = (0i);",
                "List Int temp = (1i);",
                "List Float temp = (10i);",
                "List Int temp = [];",
                "List Int temp = [1i];",
            };


            for (int i = 0; i < code.Length; i++)
            {
                Parser parser = new Parser();
                string statement = code[i];

                AbstractSyntaxTreeNode ast = parser.Parse(statement);

                string AST = ast.ToAbstractSyntaxTree();
                string STR = ast.ToString();

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ListDeclarationStatement expressionStmnt = parser.Program.Block.Statements[0] as ListDeclarationStatement;
                Assert.NotNull(expressionStmnt);
            }
        }

        [Fact]
        public void Invalid()
        {
            string[][] code =
            [
                ["List Int primes = [;","Token 'Semicolon' was unexpected."],
                ["List Float primes = ];", "Parser expected 'LeftSquareBracket' or 'LeftParenthesis' token but acutal token was 'RightSquareBracket'."],
                ["List Float pies =;", "Parser expected 'LeftSquareBracket' or 'LeftParenthesis' token but acutal token was 'Semicolon'."],
                ["List String names;", "Parser expected 'Assign' token but actual token was 'Semicolon'."],
                ["List Int temp = (;", "Parser expected 'Int' token but actual token was 'Semicolon'."],
                ["List Int temp = );", "Parser expected 'LeftSquareBracket' or 'LeftParenthesis' token but acutal token was 'RightParenthesis'."],
                ["List Float temp = ();", "Parser expected 'Int' token but actual token was 'RightParenthesis'."],
            ];

            for (int i = 0; i < code.Length; i++)
            {
                string input = code[i][0];
                string error = code[i][1];

                Parser parser = new Parser();
                AbstractSyntaxTreeNode ast = parser.Parse(input);

                string AST = ast.ToAbstractSyntaxTree();
                string STR = ast.ToString();

                if (!STR.Contains(error))
                {
                    Assert.Fail($"Statement: '{input}' should fail with message: '{error}' but it didn't");
                }
            }
        }

        [Fact]
        public void Methods()
        {
            string executionLocation = GetParserSourceFile("list.eden");

            Parser parser = new Parser();
            AbstractSyntaxTreeNode ast = parser.ParseFile(executionLocation);

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();

            Console.WriteLine(STR);
        }
    }
}
