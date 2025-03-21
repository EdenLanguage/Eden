using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using System.Text;

namespace EdenTests.EvaluatorTests
{
    public class Float
    {
        [Fact]
        public void Evaluation()
        {
            string[][] testset =
            [
                ["2.5f + 3.5f;", "6"],
                ["10.0f - 4.5f;", "5.5"],
                ["3.0f * 2.5f;", "7.5"],
                ["9.0f / 2.0f;", "4.5"],
                ["5.5f + 1.5f;", "7"]
            ];

            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);

                if (expected != result.AsString())
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Expression: '{input}' failed!");
                    sb.AppendLine($"Expected: '{expected}'");
                    sb.AppendLine($"Actual: '{result}'");

                    Assert.Fail(sb.ToString());
                }
            }
        }
    }
}
