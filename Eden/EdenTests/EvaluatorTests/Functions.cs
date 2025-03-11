using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Functions : FileTester
    {
        [Fact]
        public void FunctionDeclaration()
        {
            string filename = "main19.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string STR = block.ToString();
            string AST = block.ToASTFormat();

            IObject result = evaluator.Evaluate(block, env);
        }

        [Fact]
        public void LengthFile()
        {
            string filename = "main25.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string STR = block.ToString();
            string AST = block.ToASTFormat();

            IObject result = evaluator.Evaluate(block, env);
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
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

                Assert.True(result is IntObject);
                Assert.Equal(expected, (result as IntObject).Value.ToString());
            }
        }

        [Fact]
        public void Sinus()
        {
            string[] code =
            [
                //  Arguments as radians
                "SinusR(0f);",
                "SinusR(30f);",
                "SinusR(45f);",
                "SinusR(60f);",
                "SinusR(90f);",

                //  Arguments as degrees
                "SinusD(0f);",
                "SinusD(30f);",
                "SinusD(45f);",
                "SinusD(60f);",
                "SinusD(90f);",
            ];

            float[] expectedResults =
            [
                //  Result from radians
                0f,
                -0.9880316f,
                0.8509035f,
                -0.3048106f,
                0.89399666f,

                //  Result from degrees
                0f,
                0.5f,
                0.70710677f,
                0.86602545f,
                1f,

            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            float expected = 0f;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

                Assert.True(result is FloatObject);
                Assert.Equal(expected, (result as FloatObject).Value);
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
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

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
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

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
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

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
                "PrintLine(5i);",
                "PrintLine(3.14f);",
                "PrintLine(\"Hello\");",
                "PrintLine(\"Hello world!\");",
            ];

            string[] expectedResults =
            [
                "5",
                "3.14",
                "Hello",
                "Hello world!",
            ];

            Assert.Equal(code.Length, expectedResults.Length);


            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                input = code[i];
                expected = expectedResults[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.Parse(input);
                string STR = block.ToString();
                string AST = block.ToASTFormat();

                IObject result = evaluator.Evaluate(block, env);

                
                Assert.True(result is StringObject);
                Assert.Equal(expected, (result as StringObject).Value);
            }
        }
    }
}