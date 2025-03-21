using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;

namespace EdenTests.ErrorTests
{
    public class Lexical
    {
        [Fact]
        public void IllegalToken()
        {
            string[][] testset =
            [
                ["dssd0.0009dssd;", ""],
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
