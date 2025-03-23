using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Return : FileTester
    {
        [Fact]
        public void ExamplaryFiles()
        {
            string[][] testset =
            [
                [GetEvaluatorSourceFile("return1.eden"),"10"],
                [GetEvaluatorSourceFile("return2.eden"),"200"],
                [GetEvaluatorSourceFile("return3.eden"),"20"],
                [GetEvaluatorSourceFile("return4.eden"),"1"],
                [GetEvaluatorSourceFile("return5.eden"),"100"],
            ];

            foreach(string[] set in testset)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (expected != STR)
                {
                    if(result is ErrorObject)
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

        [Fact]
        public void Test_1()
        {
            string input = 
                "10i+20i*3i;" +
                "Return 10i;" +
                "100i;";

            string expected = "10";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(input);

            Assert.True(result is IntObject);

            Assert.True((result as IntObject).Value == 10);
        }

        [Fact]
        public void Test_2()
        {
            string input =
                "10i+23i*3i;" +
                "Return 5i*10i-1i*(8i*2i);" +
                "10123120i;";

            string expected = "10";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.Evaluate(input);

            Assert.True(result is IntObject);

            Assert.True((result as IntObject).Value == 34);
        }
    }
}
