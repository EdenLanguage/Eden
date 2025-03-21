using System.Text;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;

namespace EdenTests.EvaluatorTests
{
    public class Char : FileTester
    {
        [Fact]
        public void Evaluation()
        {
            string[][] testset =
            [
                ["0c;","0"],
                ["1c;","1"],
                ["100c;","100"],
                ["101c;","101"],
                ["254c;","254"],
                ["255c;","255"],
                ["256c;","0"],
                ["'1';","49"],
                ["'2';","50"],
                ["'5';","53"],
                ["'9';","57"],

                ["100c + 100c;", "200"],
                ["200c + 100c;", "45"],
                ["254c + 2c;", "1"],
                ["250c + 10c;", "5"],
                ["0c + 255c;", "0"],

                ["100c - 50c;", "50"],
                ["10c - 15c;", "250"],
                ["5c - 10c;", "250"],
                ["254c - 255c;", "254"],
                ["1c - 2c;", "254"],

                ["10c * 10c;", "100"],
                ["20c * 20c;", "145"],
                ["30c * 10c;", "45"],
                ["16c * 16c;", "1"],
                ["200c * 2c;", "145"],

                ["100c / 2c;", "50"],
                ["200c / 10c;", "20"],
                ["255c / 5c;", "51"],
                ["16c / 4c;", "4"],
                ["1c / 2c;", "0"],

                ["100c < 200c;", "True"],
                ["255c > 1c;", "True"],
                ["50c >= 50c;", "True"],
                ["10c <= 5c;", "False"],
                ["30c == 30c;", "True"],
                ["40c != 50c;", "True"],

                ["255c + 1c;", "1"],
                ["128c + 128c;", "1"],
                ["254c - 255c;", "254"],
                ["254c * 2c;", "253"],
                ["254c / 2c;", "127"],

                ["'A' + 'B';", "131"],
                ["'0' + '0';", "96"],
                ["'X' + 'Y';", "177"],
                ["'1' + '2';", "99"],
                ["'z' + 'a';", "219"],
                ["'~' + 'A';", "191"],
                ["'ÿ' + '1';", "49"],

                ["'C' - 'A';", "2"],
                ["'9' - '0';", "9"],
                ["'z' - 'a';", "25"],
                ["'ÿ' - 'A';", "190"],
                ["'B' - 'Z';", "231"],

                ["'A' * '\u0002';", "130"],
                ["'B' * '\u0003';", "198"],
                ["'C' * '\u0004';", "13"],
                ["'5' * '3';", "153"],

                ["'D' / '\u0002';", "34"],
                ["'H' / '\u0004';", "18"],
                ["'Z' / '\u0005';", "18"],
                ["'ÿ' / 10c;", "25"],

                ["'A' < 'B';", "True"],
                ["'z' > 'a';", "True"],
                ["'X' >= 'X';", "True"],
                ["'9' <= '0';", "False"],
                ["'A' == 'A';", "True"],
                ["'B' != 'C';", "True"],
                ["'ÿ' > 'A';", "True"],
                ["'A' > 'ÿ';", "False"],
                ["'Z' < 'a';", "True"],
            ];

            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                AbstractSyntaxTreeNode ast = parser.Parse(input);

                string AST = ast.ToAbstractSyntaxTree();
                string STR = ast.ToString();

                IObject actual = evaluator.Evaluate(ast);

                if(expected != actual.AsString())
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Expression: '{input}' failed!");
                    sb.AppendLine($"Expected: '{expected}'");
                    sb.AppendLine($"Actual: '{actual}'");

                    Assert.Fail(sb.ToString());
                }
            }
        }

        [Fact]
        public void Expressions()
        {
            string[][] testset =
            [
                ["0c + 100c;","100"],
                ["255c + 1c;","1"],
                ["0c - 255c;","0"],
                ["0c - 100c;","155"],
                ["10c + 10c;","20"],
                ["10c - 10c;","0"],
                ["10c * 10c;","100"],
                ["1c * 115c;","115"],
                ["115c * 115c;","220"],
                ["10c / 10c;","1"],
                ["(2c * 5c) + 10c;","20"],
                ["((2c * 50c) - 100c) + 1c;","1"],
            ];

            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject actual = evaluator.Evaluate(input);

                if (expected != actual.AsString())
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Expression: '{input}' failed!");
                    sb.AppendLine($"Expected: '{expected}'");
                    sb.AppendLine($"Actual: '{actual}'");

                    Assert.Fail(sb.ToString());
                }
            }
        }
    }
}
