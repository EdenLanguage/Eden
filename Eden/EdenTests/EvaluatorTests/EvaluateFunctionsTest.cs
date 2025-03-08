using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class EvaluateFunctionsTest : FileTester
    {
        [Fact]
        public void Function_1()
        {
            string filename = "main19.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string STR = block.ToString();
            string AST = block.ToASTFormat();

            IObject result = evaluator.Evaluate(block, env);
        }
    }
}
