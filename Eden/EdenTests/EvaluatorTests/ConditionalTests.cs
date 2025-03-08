using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class ConditionalTests
    {
        [Fact]
        public void If()
        {
            string[] input = new string[]
            {
                "If(2>1){10;};",
                "If(2<1){10;};",
                "If(False){100;};",
            };

            string[] expectedOutput = new string[]
            {
                "10",
                "Null",
                "Null",
            };

            Assert.Equal(input.Length, expectedOutput.Length);

            for (int i = 0; i < input.Length; i++)
            {
                string inputCode = input[i];
                string expected = expectedOutput[i];

                Parser parser = new Parser();
                FileStatement output = parser.Parse(inputCode);

                Assert.True(parser.Errors.Length == 0);

                string AST = output.ToString();
                string toSTR = output.ToASTFormat();

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                IObject result = evaluator.Evaluate(output, env);

                string actualAsString = result.AsString();

                Assert.Equal(expected, actualAsString);
            }
        }

        [Fact]
        public void IfElse()
        {
            string[] input = new string[]
            {
                "If(2>1){10;}Else{20;};",
                "If(2<1){10;}Else{20;};",
                "If(True){100;}Else{20;};",
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
                FileStatement output = parser.Parse(inputCode);

                Assert.True(parser.Errors.Length == 0);

                string AST = output.ToString();
                string toSTR = output.ToASTFormat();

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                IObject result = evaluator.Evaluate(output, env);

                string actualAsString = result.AsString();

                Assert.Equal(expected, actualAsString);
            }
        }
    }
}
