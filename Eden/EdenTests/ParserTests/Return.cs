using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.ParserTests
{
    public class Return : FileTester
    {
        [Fact]
        public void Valid()
        {
            string[] codes = new string[]
            {
                "Return 50i;",
                "Return False;",
                "Return Bool;",
                "Return INt;",
                "Return IN5sd5",
                "Return zmienna;",
                "Return zm6ienna;",
                "Return 50i*50i;",
                "Return 0.1f+0.2f;",
            };

            string[] expecteds = new string[]
            {
                "50",
                "False",
                null,
                "INt",
                null,
                "zmienna",
                null,
                "(50*50)",
                "(0.1+0.2)",
            };

            bool equal = codes.Length == expecteds.Length;
            Assert.True(equal);

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < codes.Length; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                if(expected == null)
                {
                    //  Invalid
                    Assert.Equal(parser.Program.Block.Statements.Length, 1);
                    Assert.Equal(parser.Errors.Length, 1);
                    Assert.True(parser.Program.Block.Statements[0] is InvalidStatement);
                }
                else
                {
                    //  Valid
                    Assert.Equal(parser.Program.Block.Statements.Length, 1);
                    Assert.Equal(parser.Errors.Length, 0);
                    Assert.True(parser.Program.Block.Statements[0] is not InvalidStatement);

                    ReturnStatement vds = parser.Program.Block.Statements[0] as ReturnStatement;
                }
            }
        }

        [Fact]
        public void NestedBlockReturn()
        {
            string filename = "main14.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            IObject result = evaluator.Evaluate(block, env);
            Assert.Equal((result as IntObject).Value, 1);

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }
    }
}
