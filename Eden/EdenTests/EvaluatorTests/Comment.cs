using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

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
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            if(result is ErrorObject AsError)
            {
                Assert.Fail($"'main27.eden' could not be parsed! Result: '{AsError.ToString()}'");
            }
        }
    }
}
