using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.Utility;
using System.Diagnostics;
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
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

            FileStatement block = parser.ParseFile(executionLocation) as FileStatement;
            string AST = block.ToAbstractSyntaxTree();
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

        [Fact]
        public void ValidLoops()
        {
            string[][] data =
            [
                [GetValidLoopsSourceFile("loop1.eden"),"10"],
                [GetValidLoopsSourceFile("loop2.eden"),"1000"],
                [GetValidLoopsSourceFile("loop3.eden"),"aaaaaaaaaa"],
                [GetValidLoopsSourceFile("loop4.eden"),"0"],
                [GetValidLoopsSourceFile("loop5.eden"),"10"],
                [GetValidLoopsSourceFile("sisyphus1.eden"),"100"],
                [GetValidLoopsSourceFile("sisyphus2.eden"),"1024"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];
                string actual = string.Empty;

                Stopwatch sw = Stopwatch.StartNew();

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                FileStatement block = parser.ParseFile(input) as FileStatement;
                IObject result = evaluator.Evaluate(block, env);

                sw.Stop();

                float seconds = ((float)sw.ElapsedMilliseconds / 1000);

                string AST = block.ToAbstractSyntaxTree();
                string STR = block.ToString();

                actual = result.AsString();

                if (result is ErrorObject IsError)
                {
                    Assert.Fail($"Program in file '{input}' could not be evaluated! Errors: {parser.PrintErrors()}");
                }
                else
                {
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void InvalidLoops()
        {
            string[][] data =
            [
                [GetInvalidLoopsSourceFile("loop1.eden"),"Parser expected 'RightParenthesis' token but acutal token was 'Semicolon'. File: 'loop1.eden' Line: '6' Column: '41'"],
                [GetInvalidLoopsSourceFile("loop2.eden"),"Parser expected 'Semicolon' token but acutal token was 'Comma'. File: 'loop2.eden' Line: '6' Column: '29'"],
                [GetInvalidLoopsSourceFile("loop3.eden"),"Parser expected 'Semicolon' token but acutal token was 'Identifier'. File: 'loop3.eden' Line: '6' Column: '30'\r\n"],
                [GetInvalidLoopsSourceFile("loop4.eden"),"Parser expected 'Semicolon' token but acutal token was 'Return'. File: 'loop4.eden' Line: '10' Column: '1'"],
                [GetInvalidLoopsSourceFile("loop5.eden"),"Parser expected 'Var' token but acutal token was 'Semicolon'. File: 'loop5.eden' Line: '7' Column: '6'"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];
                string actual = string.Empty;

                Stopwatch sw = Stopwatch.StartNew();

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();

                AbstractSyntaxTreeNode block = parser.ParseFile(input);
                IObject result = evaluator.Evaluate(block, env);

                sw.Stop();

                float seconds = ((float)sw.ElapsedMilliseconds / 1000);

                string AST = block.ToAbstractSyntaxTree();
                string STR = block.ToString();

                actual = result.AsString();

                if (result is ErrorObject IsError)
                {
                    if (!actual.Contains(expected))
                    {
                        Assert.Fail($"Program '{input}' failed but output does not match!");
                    }
                }
                else
                {
                    Assert.Fail($"Program '{input}' was evaluated but was not supposed to!");
                }
            }
        }
    }
}
