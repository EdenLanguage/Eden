using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class String : FileTester
    {
        [Fact]
        public void Indexing()
        {
            string filename = "main34.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(parser.Errors.Length == 0);
        }
    }
}
