using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Evaluation : FileTester
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
                "?10i;",
                "?0i;",
                "?False;",
                "~True;",
                "~False;",
                "~10i;",
                "!!10i;",
                "--10i;",
                "-!10i;",
                "5i;",
                "-5i;",
                "!5i;",
                "True;",
                "!False;",
                "!!False;",
                "!0i;",
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
                "(5i+5i)*10i;",
                "(5i+(1i-(-9i))*10i;",
                "(10i-12i)*(4i/2i-0i);",
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
                "5i>2i;",
                "5i<2i;",
                "5i>=4i;",
                "5i>=5i;",
                "5i<=4i;",
                "5i<=5i;",
                "--5i==-5i;",
                "5i+1i==6i;",
                "-5i==5i;",
                "1i+1i;",
                "1i-1i;",
                "1i*1i;",
                "10i/5i;",
                "10i==5i;",
                "10i!=5i;",
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
                "5i + True;",
                "5i + True; 5i;",
                "-True",
                "True + False;",
                "5i; True + False; 5i",
                "If (10i > 1i) { True + False; }",
                "if (10i > 1i) {" +
                "   If (10i > 1i) {" +
                "       Return True + False;" +
                "       }" +
                "   Return 1i;" +
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

        [Fact]
        public void VariableTypesOperations()
        {
            string[][] input =
            [
                // Char Addition (+)
                ["100c + 100c;", "200"],  // 100 + 100 = 200
                ["200c + 100c;", "45"],  // (200 + 100) % 255 = 45
                ["254c + 2c;", "1"],    // (254 + 2) % 255 = 1
                ["250c + 10c;", "5"],   // (250 + 10) % 255 = 5
                ["0c + 255c;", "0"],    // (0 + 255) % 255 = 0

                // Char Subtraction (-)
                ["100c - 50c;", "50"],   // 100 - 50 = 50
                ["10c - 15c;", "250"],    // (10 - 15) % 255 = 250
                ["5c - 10c;", "250"],     // (5 - 10) % 255 = 250
                ["254c - 255c;", "254"],  // (254 - 255) % 255 = 254
                ["1c - 2c;", "254"],      // (1 - 2) % 255 = 254

                // Char Multiplication (*)
                ["10c * 10c;", "100"],   // 10 * 10 = 100
                ["20c * 20c;", "145"],   // (20 * 20) % 255 = 145
                ["30c * 10c;", "45"],   // (30 * 10) % 255 = 45
                ["16c * 16c;", "1"],   // (16 * 16) % 255 = 1
                ["200c * 2c;", "145"],   // (200 * 2) % 255 = 145

                // Char Division (/)
                ["100c / 2c;", "50"],   // 100 / 2 = 50
                ["200c / 10c;", "20"],  // 200 / 10 = 20
                ["255c / 5c;", "51"],   // 255 / 5 = 51
                ["16c / 4c;", "4"],    // 16 / 4 = 4
                ["1c / 2c;", "0"],     // 1 / 2 = 0

                // Char Comparisons
                ["100c < 200c;", "True"],   // 100 < 200
                ["255c > 1c;", "True"],     // 255 > 1
                ["50c >= 50c;", "True"],    // 50 >= 50
                ["10c <= 5c;", "False"],    // 10 <= 5
                ["30c == 30c;", "True"],    // 30 == 30
                ["40c != 50c;", "True"],    // 40 != 50

                // Edge Cases
                ["255c + 1c;", "1"],    // (255 + 1) % 255 = 1
                ["128c + 128c;", "1"],  // (128 + 128) % 255 = 1
                ["254c - 255c;", "254"],  // (254 - 255) % 255 = 254
                ["254c * 2c;", "253"],    // (254 * 2) % 255 = 253
                ["254c / 2c;", "127"],     // 254 / 2 = 127

                // Char Addition (+)
                ["'A' + 'B';", "131"],  // 65 + 66 = 131
                ["'0' + '0';", "96"],  // 48 + 48 = 96
                ["'X' + 'Y';", "177"],  // 88 + 89 = 177
                ["'1' + '2';", "99"],  // 49 + 50 = 99
                ["'z' + 'a';", "219"],  // 122 + 97 = 219
                ["'~' + 'A';", "191"],  // 126 + 65 = 191 (No overflow)
                ["'ÿ' + '1';", "49"],  // 255 + 49 = Overflow, capped at 255

                // Char Subtraction (-)
                ["'C' - 'A';", "2"],  // 67 - 65 = 2
                ["'9' - '0';", "9"],  // 57 - 48 = 9
                ["'z' - 'a';", "25"],  // 122 - 97 = 25
                ["'ÿ' - 'A';", "190"],  // 255 - 65 = 190
                ["'B' - 'Z';", "231"],  // 66 - 90 = 231 (Overflow)

                // Char Multiplication (*)
                ["'A' * '\u0002';", "130"],  // 65 * 2 = 130
                ["'B' * '\u0003';", "198"],  // 66 * 3 = 198
                ["'C' * '\u0004';", "13"],  // 67 * 4 = 268 (Overflow, capped at 255)
                ["'5' * '3';", "153"],  // 53 * 51 = 270 (Overflow)

                // Char Division (/)
                ["'D' / '\u0002';", "34"],  // 68 / 2 = 34
                ["'H' / '\u0004';", "18"],  // 72 / 4 = 18
                ["'Z' / '\u0005';", "18"],  // 90 / 5 = 18
                ["'ÿ' / '\u000A';", "25"],  // 255 / 10 = 25

                // Char Comparisons (<, >, <=, >=, ==, !=)
                ["'A' < 'B';", "True"],   // 65 < 66
                ["'z' > 'a';", "True"],   // 122 > 97
                ["'X' >= 'X';", "True"],  // 88 >= 88
                ["'9' <= '0';", "False"], // 57 <= 48
                ["'A' == 'A';", "True"],  // 65 == 65
                ["'B' != 'C';", "True"],  // 66 != 67
                ["'ÿ' > 'A';", "True"],   // 255 > 65
                ["'A' > 'ÿ';", "False"],  // 65 > 255 (False)
                ["'Z' < 'a';", "True"],   // 90 < 97

                // Int
                ["10i + 5i;", "15"],
                ["20i - 8i;", "12"],
                ["7i * 6i;", "42"],
                ["50i / 5i;", "10"],

                // Bool
                ["5i > 3i;", "True"],

                // String
                ["\"Hello\" + \" World\";", "Hello World"],
                ["\"C#\" + \" Rocks\";", "C# Rocks"],
                ["\"Test\" + \"123\";", "Test123"],
                ["\"Open\" + \"AI\";", "OpenAI"],
                ["\"Code\" + \" Fun\";", "Code Fun"],

                // Float
                ["2.5f + 3.5f;", "6"],
                ["10.0f - 4.5f;", "5.5"],
                ["3.0f * 2.5f;", "7.5"],
                ["9.0f / 2.0f;", "4.5"],
                ["5.5f + 1.5f;", "7"]
            ];

            for (int i = 0; i < input.Length; i++)
            {
                string[] set = input[i];

                Parser parser = new Parser();
                FileStatement output = parser.Parse(set[0]);

                string AST = output.ToString();
                string toSTR = output.ToASTFormat();

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                IObject result = evaluator.Evaluate(output, env);

                Assert.True(result is not ErrorObject);

                string actualAsString = result.AsString();

                bool areEqual = set[1] == actualAsString;

                if(areEqual == false)
                {
                    Assert.Fail($"Expression '{set[0]}' returned '{actualAsString}' but it should return '{set[1]}'!");
                }
            }
        }
    }
}
