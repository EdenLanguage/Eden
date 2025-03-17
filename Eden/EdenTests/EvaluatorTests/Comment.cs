using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Comment : FileTester
    {
        [Fact]
        public void FromFile_1()
        {
            string filename = "main27.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;

            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            if(result is ErrorObject AsError)
            {
                Assert.Fail($"'main27.eden' could not be parsed! Result: '{AsError.ToString()}'");
            }
        }
    }
}
