using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.ExampleCodeTests
{
    public class Calculations : FileTester
    {
        [Fact]
        public void Fibonacci()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("fibonacci.eden"),"None"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if(actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }

        [Fact]
        public void Factorial()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("factorial.eden"),"None"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if (actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }

        [Fact]
        public void Reverse()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("reverseString.eden"),"None"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if (actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }

        [Fact]
        public void Palindrome()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("palindrome.eden"),"None"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if (actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }

        [Fact]
        public void Prime()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("primeFinder.eden"),"None"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if (actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }
    }
}
