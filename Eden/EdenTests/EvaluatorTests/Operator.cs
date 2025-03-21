using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using System.Text;

namespace EdenTests.EvaluatorTests
{
    public class Operator
    {
        [Fact]
        public void Valid()
        {
            string[][] testset =
            [
                ["True && True;", "True"],
                ["True && False;", "False"],
                ["False && True;", "False"],
                ["False && False;", "False"],
                ["True || True;", "True"],
                ["True || False;", "True"],
                ["False || True;", "True"],
                ["False || False;", "False"],
                ["(5i > 2i) && (3i < 4i);", "True"],
                ["(10i == 10i) || (5i > 20i);", "True"],
                ["(1i == 2i) || (3i == 3i);", "True"],
                ["(4i > 5i) && (6i < 7i);", "False"],
                ["(7i >= 7i) && (8i != 9i);", "True"],
                ["(0i < -1i) || (100i > 50i);", "True"],
                ["(2i * 3i == 6i) && (4i / 2i == 2i);", "True"],
                ["(5i + 5i == 10i) && (6i - 2i == 4i);", "True"],
                ["(15i / 3i == 5i) || (8i * 2i == 20i);", "True"],
                ["(3i == 3i) && (5i < 10i) || False;", "True"],
                ["(True && True) || (False && False);", "True"],
                ["(False || False) && (True && True);", "False"],
                ["(True && False) || (True && True);", "True"],
                ["(4i > 2i) && (False || True);", "True"],
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

        [Fact]
        public void Invalid()
        {
            string[][] testset =
            [
                ["5i && 5i;", "Operation '5 Illegal 5' is not defined!"],
                ["5i || 5i;", "Operation '5 Illegal 5' is not defined!"],
                ["True && 5i;", "Operation 'True Illegal 5' is not defined!"],
                ["False || 3.14f;", "Operation 'False Illegal 3.14' is not defined!"],
                ["\"text\" && \"more text\";", "Operation 'text Illegal more text' is not defined!"],
                ["\"hello\" || 10c;", "Operation 'hello Illegal 10' is not defined!"],
                ["Null && 1c;", "Operation 'Null Illegal 1' is not defined!"],
                ["Null || False;", "Operation 'Null Illegal False' is not defined!"],
                ["5i && True;", "Operation '5 Illegal True' is not defined!"],
                ["5.0f || 5i;", "Operation '5 Illegal 5' is not defined!"],
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

                if (result is ErrorObject asError)
                {
                    string error = asError.AsString();

                    if (!error.Contains(expected))
                    {
                        Assert.Fail($"Invalid output message. Should be: '{expected}' but is '{error}'");
                    }

                }
                else
                {
                    Assert.Fail($"Statement: '{input}' should fail but it didn't!");
                }
            }
        }
    }
}
