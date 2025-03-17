using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class VarDeclarationTests : FileTester
    {
        [Fact]
        public void EvaluateVarAssingmentTest_1()
        {
            string filename = "main15.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(parser.Errors.Length == 0);
        }

        [Fact]
        public void EvaluateVarAssingmentTest_2()
        {
            string filename = "main16.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(parser.Errors.Length == 0);
            Assert.True(result is IntObject);
            Assert.Equal((result as IntObject).Value, 30);
        }

        [Fact]
        public void EvaluateVarAssingmentTest_3()
        {
            string filename = "main17.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(parser.Errors.Length == 0);
            Assert.True(result is FloatObject);
            Assert.Equal((result as FloatObject).Value, 62.8000031f);
        }
    }
}
