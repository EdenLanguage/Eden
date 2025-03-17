using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenTests.Utility;

namespace EdenTests.ParserTests
{
    public class Invalid : FileTester
    {
        [Fact]
        public void Files()
        {
            string executionLocation = GetParserInvalidSourceDir();

            string[] files = Directory.GetFiles(executionLocation);

            foreach (string file in files)
            {
                Parser parser = new Parser();
                Statement ast = parser.ParseFile(file);

                if (ast is not InvalidStatement asInvalidStatement)
                {
                    Assert.Fail($"File '{file}' should fail but it didn't!");
                }
            }
        }

        [Fact]
        public void Invalid6()
        {
            string executionLocation = GetParserInvalidSourceFile("invalid6.eden");

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            if (ast is not InvalidStatement asInvalidStatement)
            {
                Assert.Fail($"File '{executionLocation}' should fail but it didn't!");
            }

            string error = ast.ToString();
        }
    }
}
