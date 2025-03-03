﻿using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;
using Xunit.Abstractions;

namespace EdenTests.ParserTests
{
    public class ExpressionTest : ConsoleWriter
    {
        public ExpressionTest(ITestOutputHelper consoleWriter) : base(consoleWriter)
        {
        }

        [Fact]
        public void Identifier()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "zmienna;",
                "counter;",
                "counter + 2;",
                "- counter + 10;",
                "counter + counter + zmienna - zmienna;",
            };

            string[] expecteds = new string[testCount]
            {
                "zmienna",
                "counter",
                "(counter+2)",
                "((-counter)+10)",
                "(((counter+counter)+zmienna)-zmienna)",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                string actual = expressionStmnt.Print();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void VariableType()
        {
            const int testCount = 3;

            string[] codes = new string[testCount]
            {
                "Int;",
                "Float;",
                "String;",
            };

            string[] expecteds = new string[testCount]
            {
                "Parser encountered invalid statement!",
                "Parser encountered invalid statement!",
                "Parser encountered invalid statement!",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 1);

                InvalidStatement invExp = parser.Program.Block.Statements[0] as InvalidStatement;
                Assert.NotNull(invExp);

                string actual = invExp.Print();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Binary()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "1+2+3;",
                "1*2+3;",
                "1+2*3;",
                "1/2*3;",
                "-1*2/3;",
            };

            string[] expecteds = new string[testCount]
            {
                "((1+2)+3)",
                "((1*2)+3)",
                "(1+(2*3))",
                "((1/2)*3)",
                "(-((1*2)/3))",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                string actual = expressionStmnt.Print();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Unary()
        {
            const int testCount = 10;

            string[] codes = new string[testCount]
            {
                "-1;",
                "+1;",
                "-52;",
                "-00001;",
                "-1424;",
                "-3.14;",
                "-0.123123;",
                "+1.5235235;",
                "0;",
                "325;",
            };

            string[] expecteds = new string[testCount]
            {
                "(-1)",
                "1",
                "(-52)",
                "(-1)",
                "(-1424)",
                "(-3.14)",
                "(-0.123123)",
                "1.5235235",
                "0",
                "325",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for(int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                string actual = expressionStmnt.Print();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Grouped()
        {
            const int testCount = 5;

            string[] codes = new string[testCount]
            {
                "(5+5)*2;",
                "5+5*2;",
                "5==(5==True);",
                "5==5==True;",
                "(1+2)*(3+4);",
            };

            string[] expecteds = new string[testCount]
            {
                "((5+5)*2);",
                "(5+(5*2));",
                "(5==(5==True));",
                "((5==5)==True);",
                "((1+2)*(3+4));",
            };

            Parser parser = new Parser();

            string code = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testCount; i++)
            {
                code = codes[i];
                expected = expecteds[i];

                parser = new Parser();
                FileStatement ast = parser.Parse(code);

                Assert.Equal(parser.Program.Block.Statements.Length, 1);
                Assert.Equal(parser.Errors.Length, 0);

                ExpressionStatement expressionStmnt = parser.Program.Block.Statements[0] as ExpressionStatement;
                Assert.NotNull(expressionStmnt);

                string actual = expressionStmnt.ToString();

                Log.WriteLine($"{expected} <- Expected");
                Log.WriteLine($"{actual} <- Actual");

                Assert.Equal(expected, actual);
            }
        }
    }
}
