using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Evaluation : FileTester
    {
        [Fact]
        public void ExemplaryFiles()
        {
            string[][] testset =
            [
                [GetTestFilesFile("main10.eden"),"5"],
                [GetTestFilesFile("main11.eden"),"True"],
                [GetTestFilesFile("main12.eden"),"Null"],
                [GetTestFilesFile("main18.eden"),"3.14"],
                [GetTestFilesFile("main20.eden"),"15"],
                [GetTestFilesFile("main21.eden"),"34"],
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