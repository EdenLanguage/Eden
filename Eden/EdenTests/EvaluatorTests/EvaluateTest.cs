using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class EvaluateTest : FileTester
    {
        [Fact]
        public void IntTest()
        {
            string filename = "main10.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToString();
            string toSTR = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is IntObject);
            IntObject value = result as IntObject;

            Assert.Equal(value.Value, 5);
        }

        [Fact]
        public void BoolTest()
        {
            string filename = "main11.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToString();
            string toSTR = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is BoolObject);
            BoolObject value = result as BoolObject;

            Assert.Equal(value.Value, true);
        }

        [Fact]
        public void NullTest()
        {
            string filename = "main12.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToString();
            string toSTR = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is NullObject);
            NullObject value = result as NullObject;

            Assert.Equal(value.Value, null);
        }

        [Fact]
        public void UnaryExpressionTest()
        {
            string[] input = new string[]
            {
                "?True;",
                "?10;",
                "?0;",
                "?False;",
                "~True;",
                "~False;",
                "~10;",
                "!!10;",
                "--10;",
                "-!10;",
                "5;",
                "-5;",
                "!5;",
                "True;",
                "!False;",
                "!!False;",
                "!0;",
            };

            string[] expectedOutput = new string[]
            {
                "False",
                "False",
                "True",
                "True",
                "False",
                "True",
                "-11",
                "10",
                "10",
                "10",
                "5",
                "-5",
                "-5",
                "True",
                "True",
                "False",
                "0",
            };

            Assert.Equal(input.Length, expectedOutput.Length);

            for(int i = 0; i < input.Length; i++)
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
        public void AdvancedUnaryExpressionTest()
        {
            string[] input = new string[]
            {
                "(5+5)*10;",
                "(5+(1-(-9))*10;",
                "(10-12)*(4/2-0);",
            };

            string[] expectedOutput = new string[]
            {
                "100",
                "105",
                "-4",
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
        public void BinaryExpressionTest()
        {
            string[] input = new string[]
            {
                "5>2;",
                "5<2;",
                "5>=4;",
                "5>=5;",
                "5<=4;",
                "5<=5;",
                "--5==-5;",
                "5+1==6;",
                "-5==5;",
                "1+1;",
                "1-1;",
                "1*1;",
                "10/5;",
                "10==5;",
                "10!=5;",
            };

            string[] expectedOutput = new string[]
            {
                "True",
                "False",
                "True",
                "True",
                "False",
                "True",
                "False",
                "True",
                "False",
                "2",
                "0",
                "1",
                "2",
                "False",
                "True",
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
        public void ExpressionErrorsTest()
        {
            string[] input = new string[]
            {
                "5 + True;",
                "5 + True; 5;",
                "-True",
                "True + False;",
                "5; True + False; 5",
                "If (10 > 1) { True + False; }",
                "if (10 > 1) {" +
                "   If (10 > 1) {" +
                "       Return True + False;" +
                "       }" +
                "   Return 1;" +
                "}"
            };


            //Assert.Equal(input.Length, expectedOutput.Length);

            for (int i = 0; i < input.Length; i++)
            {
                string inputCode = input[i];
                //string expected = expectedOutput[i];

                Parser parser = new Parser();
                FileStatement output = parser.Parse(inputCode);

                string AST = output.ToString();
                string toSTR = output.ToASTFormat();

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                IObject result = evaluator.Evaluate(output, env);

                Assert.True(result is ErrorObject);

                string actualAsString = result.AsString();

                //Assert.Equal(expected, actualAsString);
            }
        }

        [Fact]
        public void ComplexStatementsTest_1()
        {
            string filename = "main18.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string AST = block.ToString();
            string toSTR = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is FloatObject);
            FloatObject value = result as FloatObject;

            Assert.Equal(value.Value, 3.14f);
        }

        [Fact]
        public void FunctionCallsTest()
        {
            string filename = "main20.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string STR = block.ToString();
            string AST = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is IntObject);
            IntObject value = result as IntObject;

            Assert.Equal(value.Value, 15);
        }

        [Fact]
        public void FibonacciTest()
        {
            string filename = "main21.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            FileStatement block = parser.ParseFile(executionLocation);

            string STR = block.ToString();
            string AST = block.ToASTFormat();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(parser.Program, env);

            Assert.True(result is IntObject);
            IntObject value = result as IntObject;

            Assert.Equal(value.Value, 34);
        }
    }
}
