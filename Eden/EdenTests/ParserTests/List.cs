using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
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
