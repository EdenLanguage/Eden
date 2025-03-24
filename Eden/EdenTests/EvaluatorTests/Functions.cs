using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Functions : FileTester
    {
        [Fact]
        public void ExemplaryFiles()
        {
            string[][] testset =
            [
                [GetTestFilesFile("main19.eden"),"None"],
                [GetTestFilesFile("main25.eden"),"4"],
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

        [Fact]
        public void Length()
        {
            string[] code =
            [
                "List Int primes = [1i,2i,3i,4i];\r\nLength(primes);",
                "List Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i];\r\nLength(primes);",
                "List Int primes = [];\r\nLength(primes);",
                "List Int primes = (10i);\r\nLength(primes);",
                "List Int primes = (1i);\r\nLength(primes);",
            ];

            string[] expectedResults =
            [
                "4",
                "9",
                "0",
                "10",
                "1",
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            string expected = string.Empty;

            for(int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);

                Assert.True(result is IntObject);
                Assert.Equal(expected, (result as IntObject).Value.ToString());
            }
        }

        [Fact]
        public void Sinus()
        {
            string[][] testset =
            [
                ["SinusR(0f);", "0"],
                ["SinusR(30f);", "-0.9880316"],
                ["SinusR(45f);", "0.8509035"],
                ["SinusR(60f);", "-0.3048106"],
                ["SinusR(90f);", "0.89399666"],

                ["SinusD(0f);", "0"],
                ["SinusD(30f);", "0.5"],
                ["SinusD(45f);", "0.70710677"],
                ["SinusD(60f);", "0.86602545"],
                ["SinusD(90f);", "1"],
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

        [Fact]
        public void Cosinus()
        {
            string[] code =
            [
                //  Arguments as radians
                "CosinusR(0f);",
                "CosinusR(30f);",
                "CosinusR(45f);",
                "CosinusR(60f);",
                "CosinusR(90f);",

                //  Arguments as degrees
                "CosinusD(0f);",
                "CosinusD(30f);",
                "CosinusD(45f);",
                "CosinusD(60f);",
                "CosinusD(90f);",
            ];

            float[] expectedResults =
            [
                //  Result from radians
                1.0000000f,
                0.154251456f,
                0.52532199f,
                -0.95241298f,
                -0.44807362f,

                //  Result from degrees
                1.0000000f,
                0.8660254f,
                0.707106769f,
                0.49999997f,
                -4.37113883E-08f,
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            float expected = 0f;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);
                IObject result = evaluator.Evaluate(input);

                Assert.True(result is FloatObject);
                Assert.Equal(expected, (result as FloatObject).Value);
            }
        }

        [Fact]
        public void Min()
        {
            string[] code =
            [
                "List Int primes = [1i,2i,3i,4i];\r\nMin(primes);",
                "List Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i];\r\nMin(primes);",
                "List Int primes = [];\r\nMin(primes);",
                "List Int primes = (10i);\r\nMin(primes);",
                "List Int primes = (1i);\r\nMin(primes);",
            ];

            string[] expectedTypes =
            [
                "IntObject",
                "IntObject",
                "NullObject",
                "IntObject",
                "IntObject",
            ];

            string[] expectedResults =
            [
                "1",
                "1",
                "Null",
                "0",
                "0",
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            string expected = string.Empty;
            string expectedType = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];
                expectedType = expectedTypes[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);

                Assert.Equal(result.Type.Name, expectedType);

                if(result.Type.Name == "IntObject")
                {
                    Assert.Equal(expected, (result as IntObject).Value.ToString());
                }
            }
        }

        [Fact]
        public void Max()
        {
            string[] code =
            [
                "List Int primes = [1i,2i,3i,4i];\r\nMax(primes);",
                "List Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i];\r\nMax(primes);",
                "List Int primes = [];\r\nMax(primes);",
                "List Int primes = (10i);\r\nMax(primes);",
                "List Int primes = (1i);\r\nMax(primes);",
            ];

            string[] expectedTypes =
            [
                "IntObject",
                "IntObject",
                "NullObject",
                "IntObject",
                "IntObject",
            ];

            string[] expectedResults =
            [
                "4",
                "9",
                "Null",
                "0",
                "0",
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            string expected = string.Empty;
            string expectedType = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];
                expectedType = expectedTypes[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);
                IObject result = evaluator.Evaluate(input);

                Assert.Equal(result.Type.Name, expectedType);

                if (result.Type.Name == "IntObject")
                {
                    Assert.Equal(expected, (result as IntObject).Value.ToString());
                }
            }
        }

        [Fact]
        public void PrintLine()
        {
            string[] code =
            [
                "Print(5i);",
                "Print(3.14f);",
                "Print(\"Hello\");",
                "Print(\"Hello world!\");",
                "PrintLine(5i);",
                "PrintLine(3.14f);",
                "PrintLine(\"Hello\");",
                "PrintLine(\"Hello world!\");",
            ];

            for (int i = 0; i < code.Length; i++)
            {
                string input = code[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);

                Assert.True(result is NoneObject);
            }
        }

        [Fact]
        public void Inc()
        {
            string[] code =
            [
                "Inc(1i);",
                "Inc(3.14f);",
                "Inc(6c);",
                "Inc(\"Hello\");",
            ];

            string[] expectedResults =
            [
                "2",
                "4.1400003",
                "7",
                "Hello ",
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);

                Assert.Equal(expected, result.AsString());
            }
        }
    }
}