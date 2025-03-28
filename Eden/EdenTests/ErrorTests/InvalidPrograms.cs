using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.ErrorTests
{
    public class InvalidPrograms : FileTester
    {
        [Fact]
        public void ErrorPrinting()
        {
            string[][] data =
            [
                [GetErrorsSourceFile("error1.eden"),"[Syntactical error]\r\nToken 'int' is not variable type!\r\nFile: 'error1.eden', Line: '1', Column: '5'\r\n\r\nVar int zmienna = 10;\r\n    ^----------------"],
                [GetErrorsSourceFile("error2.eden"),"[Syntactical error]\r\nParser expected 'RightBracket' token but actual token was 'Semicolon'.\r\nFile: 'error2.eden', Line: '2', Column: '42'\r\n\r\nLoop(Var Int j = 0i; j < 10i; j = j + 1i;){\r\n                                        ^--"],
                [GetErrorsSourceFile("error3.eden"),"[Syntactical error]\r\nParser expected 'RightBracket' token but actual token was 'Quit'.\r\nFile: 'error3.eden', Line: '4', Column: '2'\r\n\r\nQuit;\r\n^----"],
                [GetErrorsSourceFile("error4.eden"),"[Syntactical error]\r\nParser expected 'LeftBracket' token but actual token was 'Return'.\r\nFile: 'error4.eden', Line: '2', Column: '2'\r\n\r\nReturn a*b;\r\n^----------"],
                [GetErrorsSourceFile("error5.eden"),"[Runtime error]\r\nFunction 'Lengt()' is not defined!\r\nFile: 'error5.eden', Line: '1', Column: '1'\r\n\r\nLengt(\"abracadabra\");\r\n^--------------------"],
                [GetErrorsSourceFile("error6.eden"),"[Syntactical error]\r\nToken 'int' is not variable type!\r\nFile: 'error6.eden', Line: '1', Column: '5'\r\n\r\nVar int pajacyk = 5i;\r\n    ^----------------"],
                [GetErrorsSourceFile("error7.eden"),"[Syntactical error]\r\nToken 'Stringasdasd' is not variable type!\r\nFile: 'error7.eden', Line: '1', Column: '5'\r\n\r\nVar Stringasdasd = \"Pawel\";\r\n    ^----------------------"],
                [GetErrorsSourceFile("error8.eden"),"[Syntactical error]\r\nParser expected 'Semicolon' token but actual token was 'Identifier'.\r\nFile: 'error8.eden', Line: '1', Column: '1'\r\n\r\nvar String imie = \"5553\";\r\n^------------------------"],
                [GetErrorsSourceFile("error9.eden"),"[Syntactical error]\r\nParser expected 'Semicolon' token but actual token was 'Eof'.\r\nFile: 'error9.eden', Line: '1', Column: '23'\r\n\r\nVar Int variable = 10i\r\n                      ^"],
                [GetErrorsSourceFile("error10.eden"),"[Lexical error]\r\nToken '\"sdasd;' is illegal!\r\nFile: 'error10.eden', Line: '1', Column: '22'\r\n\r\nVar String surname = \"sdasd;\r\n                     ^------"],
                [GetErrorsSourceFile("error11.eden"),"[Syntactical error]\r\nParser expected 'Identifier' token but actual token was 'Assign'.\r\nFile: 'error11.eden', Line: '2', Column: '10'\r\n\r\nVar Int =;\r\n        ^-"],
                [GetErrorsSourceFile("error12.eden"),"[Syntactical error]\r\nParser expected 'VariableType' token but actual token was 'Identifier'.\r\nFile: 'error12.eden', Line: '1', Column: '10'\r\n\r\nFunction StringConcat(Var String arg){\r\n         ^----------------------------"],
            ];

            foreach (string[] test in data)
            {
                string input = test[0];
                string expected = test[1];

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    expected = test[1].Replace("\r", "");
                }

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string actual = result.AsString();

                if(expected != actual)
                {
                    if (result is ErrorObject IsError)
                    {
                        Assert.Fail(actual);
                    }
                    else
                    {
                        Assert.Fail($"Program '{Path.GetFileName(input)}' was evaluated but was not supposed to!");
                    }
                }
            }
        }
    }
}