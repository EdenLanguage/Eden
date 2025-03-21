using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using EdenClasslibrary.Types.LanguageTypes;
using EdenTests.LexerTests;
using EdenTests.Utility;
using System.Diagnostics;
using ParsingEnvironment = EdenClasslibrary.Types.ParsingEnvironment;

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
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            if (result is ErrorObject IsError)
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
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

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
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

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
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

            if (result is not ErrorObject IsError)
            {
                Assert.Fail($"Program was evaluated '{filename}' but it shouldn't!");
            }
        }

        [Fact]
        public void FileInput_5()
        {
            string filename = "main33.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);

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
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);

                sw.Stop();

                float seconds = ((float)sw.ElapsedMilliseconds / 1000);

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
                [GetInvalidLoopsSourceFile("loop1.eden"),"Parser expected 'RightParenthesis' token but actual token was 'Semicolon'."],
                [GetInvalidLoopsSourceFile("loop2.eden"),"Parser expected 'Semicolon' token but actual token was 'Comma'."],
                [GetInvalidLoopsSourceFile("loop3.eden"),"Parser expected 'Semicolon' token but actual token was 'Identifier'."],
                [GetInvalidLoopsSourceFile("loop4.eden"),"Parser expected 'Semicolon' token but actual token was 'Return'."],
                [GetInvalidLoopsSourceFile("loop5.eden"),"Parser expected 'Var' token but actual token was 'Semicolon'."],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];
                string actual = string.Empty;

                Stopwatch sw = Stopwatch.StartNew();

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);

                sw.Stop();

                float seconds = ((float)sw.ElapsedMilliseconds / 1000);

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
