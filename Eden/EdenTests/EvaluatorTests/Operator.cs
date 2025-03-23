using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using System.Text;
using System;

namespace EdenTests.EvaluatorTests
{
    public class Operator
    {
        [Fact]
        public void Valid()
        {
            string[][] testset =
            [
                //  Modulo
                ["(7.13f % 7i == 0.13000011f);", "True"],
                ["(7.13f % 5i == 2.13000011f);", "True"],

                ["(7.13f % 7c == 0.13000011f);", "True"],
                ["(7.13f % 5c == 2.13000011f);", "True"],
                
                ["(10i % 7c == 3i);", "True"],

                ["(10i % 1i == 0i);", "True"],
                ["(10i % 2i == 0i);", "True"],
                ["(10i % 3i == 1i);", "True"],
                ["(10i % 4i == 2i);", "True"],
                ["(10i % 5i == 0i);", "True"],
                ["(10i % 6i == 4i);", "True"],
                ["(10i % 7i == 3i);", "True"],
                ["(10i % 8i == 2i);", "True"],
                ["(10i % 9i == 1i);", "True"],
                ["(10i % 10i == 0i);", "True"],

                ["True && True;", "True"],
                ["True && False;", "False"],
                ["False && True;", "False"],
                ["False && False;", "False"],
                ["True || True;", "True"],
                ["True || False;", "True"],
                ["False || True;", "True"],
                ["False || False;", "False"],
                ["(5i > 2i) && (3i < 4i);", "True"],
                ["(10i == 10i) || (5i > 20i);", "True"],
                ["(1i == 2i) || (3i == 3i);", "True"],
                ["(4i > 5i) && (6i < 7i);", "False"],
                ["(7i >= 7i) && (8i != 9i);", "True"],
                ["(0i < -1i) || (100i > 50i);", "True"],
                ["(2i * 3i == 6i) && (4i / 2i == 2i);", "True"],
                ["(5i + 5i == 10i) && (6i - 2i == 4i);", "True"],
                ["(15i / 3i == 5i) || (8i * 2i == 20i);", "True"],
                ["(3i == 3i) && (5i < 10i) || False;", "True"],
                ["(True && True) || (False && False);", "True"],
                ["(False || False) && (True && True);", "False"],
                ["(True && False) || (True && True);", "True"],
                ["(4i > 2i) && (False || True);", "True"],
            ];


            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    expected = testset[i][1].Replace("\r", "");
                }

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);
                string STR = result.ToString();

                if(result is ErrorObject asErrorObj)
                {
                    Assert.Fail(STR);
                }

                if (expected != STR)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Expression: '{input}' failed!");
                    sb.AppendLine($"Expected: '{expected}'");
                    sb.AppendLine($"Actual: '{result}'");

                    Assert.Fail(sb.ToString());
                }
            }
        }

        [Fact]
        public void Invalid()
        {
            string[][] testset =
            [
                //  Modulo
                ["5.0f % 5.0f;", "[Semantical error]\r\nOperation 'Float(5) % Float(5)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n5.0f % 5.0f;\r\n^-----------\r\n"],

                //  Logical
                ["5i && 5i;", "[Semantical error]\r\nOperation 'Int(5) && Int(5)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n5i && 5i;\r\n^--------\r\n"],
                ["5i || 5i;", "[Semantical error]\r\nOperation 'Int(5) || Int(5)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n5i || 5i;\r\n^--------\r\n"],
                ["True && 5i;", "[Semantical error]\r\nOperation 'Bool(True) && Int(5)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nTrue && 5i;\r\n^----------\r\n"],
                ["False || 3.14f;", "[Semantical error]\r\nOperation 'Bool(False) || Float(3.14)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nFalse || 3.14f;\r\n^--------------\r\n"],
                ["\"text\" && \"more text\";", "[Semantical error]\r\nOperation 'String(text) && String(more text)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n\"text\" && \"more text\";\r\n^---------------------\r\n"],
                ["\"hello\" || 10c;", "[Semantical error]\r\nOperation 'String(hello) || Char(10)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n\"hello\" || 10c;\r\n^--------------\r\n"],
                ["Null && 1c;", "[Semantical error]\r\nOperation 'Null(Null) && Char(1)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nNull && 1c;\r\n^----------\r\n"],
                ["Null || False;", "[Semantical error]\r\nOperation 'Null(Null) || Bool(False)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nNull || False;\r\n^-------------\r\n"],
                ["5i && True;", "[Semantical error]\r\nOperation 'Int(5) && Bool(True)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n5i && True;\r\n^----------\r\n"],
                ["5.0f || 5i;", "[Semantical error]\r\nOperation 'Float(5) || Int(5)' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\n5.0f || 5i;\r\n^----------\r\n"],
            ];


            string input = string.Empty;
            string expected = string.Empty;

            for (int i = 0; i < testset.Length; i++)
            {
                input = testset[i][0];
                expected = testset[i][1];

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    expected = testset[i][1].Replace("\r", "");
                }

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(input);
                string STR = result.ToString();

                if (result is ErrorObject asError)
                {
                    if (STR != expected)
                    {
                        Assert.Fail(STR);
                    }
                }
                else
                {
                    Assert.Fail($"Statement: '{input}' should fail but it didn't!");
                }
            }
        }
    }
}
