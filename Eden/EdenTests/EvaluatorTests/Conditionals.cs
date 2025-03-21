using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenTests.EvaluatorTests
{
    public class Conditionals
    {
        [Fact]
        public void If()
        {
            string[] input = new string[]
            {
                "If(2i>1i){10i;};",
                "If(2i<1i){10i;};",
                "If(False){100i;};",
            };

            string[] expectedOutput = new string[]
            {
                "10",
                "None",
                "None",
            };

            Assert.Equal(input.Length, expectedOutput.Length);

            for (int i = 0; i < input.Length; i++)
            {
                string inputCode = input[i];
                string expected = expectedOutput[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(inputCode);

                string actualAsString = result.AsString();

                Assert.Equal(expected, actualAsString);
            }
        }

        [Fact]
        public void IfElse()
        {
            string[] input = new string[]
            {
                "If(2i>1i){10i;}Else{20i;};",
                "If(2i<1i){10i;}Else{20i;};",
                "If(True){100i;}Else{20i;};",
            };

            string[] expectedOutput = new string[]
            {
                "10",
                "20",
                "100",
            };

            Assert.Equal(input.Length, expectedOutput.Length);

            for (int i = 0; i < input.Length; i++)
            {
                string inputCode = input[i];
                string expected = expectedOutput[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(inputCode);

                string actualAsString = result.AsString();

                Assert.Equal(expected, actualAsString);
            }
        }
    }
}
