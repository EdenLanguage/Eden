using EdenTests.Utility;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenTests.StandardFunctions
{
    public class ConsoleTests : FileTester
    {
        string[][] DataSet =
        [
            [GetConsoleSourceFile("console1.eden"),"None"],
        ];

        [Fact]
        public void Evaluate()
        {
            foreach (string[] test in DataSet)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if (actual != expected)
                {
                    Assert.Fail(actual);
                }
            }
        }

        [Fact]
        public void Parse()
        {
            foreach (string[] test in DataSet)
            {
                string input = test[0];
                string expected = test[1];

                Parser parser = new Parser();
                Statement statement = parser.ParseFile(input);

                string AST = statement.ToAbstractSyntaxTree();
                string STR = statement.ToString();

                Console.WriteLine(AST);
            }
        }
    }
}
