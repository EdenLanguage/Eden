using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using System.Text;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Int
    {
        [Fact]
        public void Evaluation()
        {
            string[][] testset =
            [
                ["0i;","0"],
                ["1i;","1"],
                ["10i;","10"],
                ["1061i;","1061"],
                ["2542i;","2542"],
                ["2535i;","2535"],
                ["2565i;","2565"],
                ["-255i;","-255"],
                ["-2545i;","-2545"],
                ["-25245i;","-25245"],
                ["-25455i;","-25455"],

                ["100i + 100i;", "200"],
                ["200i + 100i;", "300"],
                ["254i + 2i;", "256"],
                ["250i + 10i;", "260"],
                ["0i + 255i;", "255"],

                ["100i - 50i;", "50"],
                ["10i - 15i;", "-5"],
                ["5i - 10i;", "-5"],
                ["254i - 255i;", "-1"],
                ["1i - 2i;", "-1"],

                ["10i * 10i;", "100"],
                ["20i * 20i;", "400"],
                ["30i * 10i;", "300"],
                ["16i * 16i;", "256"],
                ["200i * 2i;", "400"],

                ["100i / 2i;", "50"],
                ["200i / 10i;", "20"],
                ["255i / 5i;", "51"],
                ["16i / 4i;", "4"],
                ["1i / 2i;", "0"],

                ["100i < 200i;", "True"],
                ["255i > 1i;", "True"],
                ["50i >= 50i;", "True"],
                ["10i <= 5i;", "False"],
                ["30i == 30i;", "True"],
                ["40i != 50i;", "True"],

                ["255i + 1i;", "256"],
                ["128i + 128i;", "256"],
                ["254i - 255i;", "-1"],
                ["254i * 2i;", "508"],
                ["254i / 2i;", "127"],

                ["0i + 100i;","100"],
                ["255i + 1i;","256"],
                ["0i - 255i;","-255"],
                ["0i - 100i;","-100"],
                ["10i + 10i;","20"],
                ["10i - 10i;","0"],
                ["10i * 10i;","100"],
                ["1i * 115i;","115"],
                ["115i * 115i;","13225"],
                ["10i / 10i;","1"],
                ["(2i * 5i) + 10i;","20"],
                ["((2i * 50i) - 100i) + 1i;","1"],

                ["10i + 5i;", "15"],
                ["20i - 8i;", "12"],
                ["7i * 6i;", "42"],
                ["50i / 5i;", "10"],
                ["5i > 3i;", "True"],

                ["(5i+5i)*10i;", "100"],
                ["(5i+(1i-(-9i))*10i;", "105"],
                ["(10i-12i)*(4i/2i-0i);", "-4"],

                ["5i>2i;", "True"],
                ["5i<2i;", "False"],
                ["5i>=4i;", "True"],
                ["5i<=4i;", "False"],
                ["--5i==-5i;", "False"],
                ["5i+1i==6i;", "True"],
                ["-5i==5i;", "False"],

                ["?True;", "False"],
                ["?10i;", "False"],
                ["?0i;", "True"],
                ["?False;", "True"],
                ["~True;", "False"],
                ["~False;", "True"],
                ["~10i;", "-11"],
                ["!!10i;", "10"],
                ["--10i;", "10"],
                ["-!10i;", "10"],
                ["5i;", "5"],
                ["-5i;", "-5"],
                ["!5i;", "-5"],
                ["True;", "True"],
                ["!False;", "True"],
                ["!!False;", "False"],
                ["!0i;", "0"]
            ];

            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                Parser parser = new Parser();

                AbstractSyntaxTreeNode ast = parser.Parse(input);

                string AST = ast.ToAbstractSyntaxTree();
                string STR = ast.ToString();

                IObject actual = evaluator.Evaluate(ast, env);

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