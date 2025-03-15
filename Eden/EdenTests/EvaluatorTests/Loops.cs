using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Loops : FileTester
    {
        [Fact]
        public void FileInput_1()
        {
            string filename = "main29.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            
            IObject result = evaluator.Evaluate(block, env);
        
            if(result is ErrorObject IsError)
            {
                Assert.Fail($"Program in file '{filename}' could not be evaluated!");
            }
        }

        [Fact]
        public void FileInput_2()
        {
            string filename = "main30.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();


            IObject result = evaluator.Evaluate(block, env);

            if (result is ErrorObject IsError)
            {
                Assert.Fail($"Program in file '{filename}' could not be evaluated!");
            }
            else
            {
                Assert.True((result as IntObject).Value == 1000);
            }
        }

        [Fact]
        public void FileInput_3()
        {
            string filename = "main31.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();


            IObject result = evaluator.Evaluate(block, env);

            if (result is ErrorObject IsError)
            {
                Assert.Fail($"Program in file '{filename}' could not be evaluated!");
            }
            else
            {
                Assert.True((result as IntObject).Value == 0);
            }
        }

        [Fact]
        public void FileInput_4()
        {
            string filename = "main32.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();


            IObject result = evaluator.Evaluate(block, env);

            if (result is ErrorObject IsError)
            {
                Assert.Fail($"Program in file '{filename}' could not be evaluated!");
            }
            else
            {
                Assert.True((result as IntObject).Value == 10);
            }
        }

        [Fact]
        public void FileInput_5()

        {
            string filename = "main33.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator();
            Environment env = new Environment();

            FileStatement block = parser.ParseFile(executionLocation);
            string AST = block.ToASTFormat();
            string STR = block.ToString();

            IObject result = evaluator.Evaluate(block, env);

            if (result is ErrorObject IsError)
            {
                Assert.Fail($"Program in file '{filename}' could not be evaluated!");
            }
            else
            {
                Assert.True((result as IntObject).Value == 100);
            }
        }
    }
}
