using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.ParserTests
{
    public class BuildInFunctions
    {
        [Fact]
        public void Length()
        {
            string code = "Length(\"Test\");";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement output = parser.Parse(code);

            string AST = output.ToASTFormat();
            string STR = output.ToString();

            IObject result = evaluator.Evaluate(output, env);

            Assert.True(result is not ErrorObject);
        }
    }
}
