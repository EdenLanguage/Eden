using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Char : FileTester
    {
        [Fact]
        public void Char1()
        {
            string filename = "char1.eden";
            string executionLocation = GetParserSourceFile(filename);

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            if(ast is InvalidStatement asInvalidStatement)
            {
                Assert.Fail(asInvalidStatement.Print());
            }

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();
        }
    }
}