using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class ReturnTests
    {
        [Fact]
        public void Test_1()
        {
            string input = 
                "10+20*3;" +
                "Return 10;" +
                "100;";

            string expected = "10";

            Parser parser = new Parser();
            FileStatement output = parser.Parse(input);

            string AST = output.ToASTFormat();
            string STR = output.ToString();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(output, env);

            Assert.True(result is IntObject);

            Assert.True((result as IntObject).Value == 10);
        }

        [Fact]
        public void Test_2()
        {
            string input =
                "10+23*3;" +
                "Return 5*10-1*(8*2);" +
                "10123120;";

            string expected = "10";

            Parser parser = new Parser();
            FileStatement output = parser.Parse(input);

            string AST = output.ToASTFormat();
            string STR = output.ToString();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(output, env);

            Assert.True(result is IntObject);

            Assert.True((result as IntObject).Value == 34);
        }
    }
}
