using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class IndexOperation : FileTester
    {
        [Fact]
        public void GetArrayIndex()
        {
            string filename = "main24.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(parser.Errors.Length == 0);
        }

        [Fact]
        public void InvalidIndex()
        {
            string code = "List Int numbers = [1, 2, 3, 4];" +
                          "Var Int v = numbers[5];";        

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.Parse(code);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(result is ErrorObject);
        }

        [Fact]
        public void NegativeIndex()
        {
            string code = "List Int numbers = [1, 2, 3, 4];" +
                          "Var Int v = numbers[-5];";      

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.Parse(code);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(result is ErrorObject);
        }

        [Fact]
        public void InvalidTypeIndex()
        {
            string code = "List Int numbers = [1, 2, 3, 4];" +
                          "Var Int v = numbers[3.14];";       

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.Parse(code);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(result is ErrorObject);
        }
        [Fact]
        public void NotIndexableObject()
        {
            string code = "Var Int v = 5[0];";

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.Parse(code);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            Assert.True(result is ErrorObject);
        }
    }
}