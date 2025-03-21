using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Functions : FileTester
    {
        [Fact]
        public void Definitions()
        {
            string filename = "funcDefinitions1.eden";
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

        [Fact]
        public void Calls()
        {
            string filename = "funcCalls1.eden";
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
