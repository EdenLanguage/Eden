using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

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

            foreach (string[] set in testset)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);
                string STR = result.ToString();

                if (expected != STR)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        string fileName = Path.GetFileName(input) + " -> ";
                        Assert.Equal(fileName + expected, fileName + STR);
                    }
                }
            }
        }
    }
}
