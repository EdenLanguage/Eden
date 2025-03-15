using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Return
    {
        [Fact]
        public void Test_1()
        {
            string input = 
                "10i+20i*3i;" +
                "Return 10i;" +
                "100i;";

            string expected = "10";

            Parser parser = new Parser();
            FileStatement output = parser.Parse(input) as FileStatement;

            string AST = output.ToAbstractSyntaxTree();
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
                "10i+23i*3i;" +
                "Return 5i*10i-1i*(8i*2i);" +
                "10123120i;";

            string expected = "10";

            Parser parser = new Parser();
            FileStatement output = parser.Parse(input) as FileStatement;

            string AST = output.ToAbstractSyntaxTree();
            string STR = output.ToString();

            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();
            IObject result = evaluator.Evaluate(output, env);

            Assert.True(result is IntObject);

            Assert.True((result as IntObject).Value == 34);
        }
    }
}
