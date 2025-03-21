using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Indexer : FileTester
    {
        [Fact]
        public void GetArrayIndex()
        {
            string filename = "main24.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(parser.Errors.Length == 0);
        }

        [Fact]
        public void InvalidIndex()
        {
            string code = "List Int numbers = [1i, 2i, 3i, 4i];" +
                          "Var Int v = numbers[5i];";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(code);

            Assert.True(result is ErrorObject);
        }

        [Fact]
        public void NegativeIndex()
        {
            string code = "List Int numbers = [1i, 2i, 3i, 4i];" +
                          "Var Int v = numbers[-5i];";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(code);

            Assert.True(result is ErrorObject);
        }

        [Fact]
        public void InvalidTypeIndex()
        {
            string code = "List Int numbers = [1i, 2i, 3i, 4i];" +
                          "Var Int v = numbers[3.14f];";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(code);

            Assert.True(result is ErrorObject);
        }
        [Fact]
        public void NotIndexableObject()
        {
            string code = "Var Int v = 5i[0i];";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(code);

            Assert.True(result is ErrorObject);
        }
    }
}