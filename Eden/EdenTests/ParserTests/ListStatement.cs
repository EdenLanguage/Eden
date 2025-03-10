using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class ListStatement : FileTester
    {
        [Fact]
        public void Declaration()
        {
            string[] code = new string[]
            {
                "List Int primes = [2,3,5,7,9];",
                "List Float primes = [2, 1.5, \"John\"];",
                "List Float pies = [1.1,2.2,3.3,4.4,5.5];",
                "List String names = [ \"Mark\" ,\"Adam\",\"Jordan\",\"David\",\"Isaac\"];",
                "List Int temp = (0);",
                "List Int temp = (1);",
                "List Float temp = (10);",
                "List Int temp = [];",
                "List Int temp = [1];",
            };

            Parser parser = new Parser();

            string statement = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                statement = code[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(statement);

                string AST = ast.ToASTFormat();
                string STR = ast.ToString();

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ListDeclarationStatement expressionStmnt = parser.Program.Block.Statements[0] as ListDeclarationStatement;
                Assert.NotNull(expressionStmnt);
            }
        }
    }
}
