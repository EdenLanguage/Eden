using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Evaluation : FileTester
    {
        [Fact]
        public void IntTest()
        {
            string filename = "main10.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(result is IntObject);
            IntObject value = result as IntObject;

            Assert.Equal(value.Value, 5);
        }

        [Fact]
        public void BoolTest()
        {
            string filename = "main11.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(result is BoolObject);
            BoolObject value = result as BoolObject;

            Assert.Equal(value.Value, true);
        }

        [Fact]
        public void NullTest()
        {
            string filename = "main12.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(result is NullObject);
            NullObject value = result as NullObject;

            Assert.Equal(value.Value, null);
        }

        [Fact]
        public void ComplexStatementsTest_1()
        {
            string filename = "main18.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(result is FloatObject);
            FloatObject value = result as FloatObject;

            Assert.Equal(value.Value, 3.14f);
        }

        [Fact]
        public void FunctionCallsTest()
        {
            string filename = "main20.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);
            string str = result.ToString();

            Assert.Equal("15", str);
        }

        [Fact]
        public void FibonacciTest()
        {
            string filename = "main21.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            Assert.True(result is IntObject);
            IntObject value = result as IntObject;

            Assert.Equal(value.Value, 34);
        }
    }
}