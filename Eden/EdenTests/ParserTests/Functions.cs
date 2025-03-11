using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.ParserTests
{
    public class Functions : FileTester
    {
        [Fact]
        public void SimpleCall()
        {
            string filename = "main7.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 5);
        }

        [Fact]
        public void Function_1()
        {
            string filename = "main5.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void Function_2()
        {
            string filename = "main6.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToString();

            Assert.True(parser.Errors.Length == 0);
            Assert.True(block.Block.Statements.Length == 1);
        }

        [Fact]
        public void Length()
        {
            string code = "Length(\"Test\");";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement output = parser.Parse(code);

            string AST = output.ToASTFormat();
            string STR = output.ToString();

            IObject result = evaluator.Evaluate(output, env);

            Assert.True(result is not ErrorObject);
        }
    }
}
