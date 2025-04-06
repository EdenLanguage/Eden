﻿using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;

namespace EdenTests.ExampleCodeTests
{
    public class SpiningDonut : FileTester
    {
        [Fact]
        public void Evaluate()
        {
            string[][] data =
            [
                [GetExampleCodeSourcePath("donut.eden"),"None"],
            ];

            foreach (string[] test in data)
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
            string[][] data =
            [
                [GetExampleCodeSourcePath("donut.eden"),"None"],
            ];

            foreach (string[] test in data)
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
