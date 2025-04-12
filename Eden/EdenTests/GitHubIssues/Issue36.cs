using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.GitHubIssues
{
    public class Issue36 : FileTester
    {
        [Fact]
        public void ExemplaryFiles()
        {
            string[][] testset =
            [
                [GetGitIssuesSourcePath("36", "main1.eden"),"None"],
            ];

            foreach (string[] set in testset)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
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
