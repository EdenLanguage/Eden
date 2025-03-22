using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using EdenTests.Utility;
using System.Linq.Expressions;

namespace EdenTests.EvaluatorTests
{
    public class List : FileTester
    {
        [Fact]
        public void Methods()
        {
            string executionLocation = GetEvaluatorSourceFile("list.eden");

            Parser parser = new Parser();
            Evaluator evaluator = new Evaluator(parser);

            IObject result = evaluator.EvaluateFile(executionLocation);
            string STR = result.ToString();

            if (result is ErrorObject IsError)
            {
                Assert.Fail(STR);
            }
        }

        [Fact]
        public void LengthMethod()
        {
            string declaration1 = "List Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];";
            string declaration2 = "List Int primes = [1i,2i,3i,4i,5i];";
            string declaration3 = "List Int primes = [];";
            string declaration4 = "List Int primes = (10i);";
            string declaration5 = "List Int primes = (5i);";
            string declaration6 = "List Int primes = (1i);";
            string declaration7 = "List Int primes = (0i);";

            string[][] dataset =
            [
                [declaration1 + "primes.Length();", "10"],
                [declaration2 + "primes.Length();", "5"],
                [declaration3 + "primes.Length();", "0"],
                [declaration4 + "primes.Length();", "10"],
                [declaration5 + "primes.Length();", "5"],
                [declaration6 + "primes.Length();", "1"],
                [declaration7 + "primes.Length();", "0"],

                [declaration6 + "primes.Length()", "[Syntactical error]\r\nParser expected 'Semicolon' token but actual token was 'RightParenthesis'.\r\nFile: 'REPL', Line: '1', Column: '38'\r\n\r\nList Int primes = (1i);primes.Length()\r\n                                     ^\r\n"],
                [declaration6 + "primes.Length)", "[Syntactical error]\r\nParser expected 'LeftParenthesis' token but actual token was 'RightParenthesis'.\r\nFile: 'REPL', Line: '1', Column: '37'\r\n\r\nList Int primes = (1i);primes.Length)\r\n                                    ^\r\n"],
                [declaration6 + "primes.Lenght();", "[Semantical error]\r\nThere is no definition for 'Lenght()' function!\r\nFile: 'REPL', Line: '1', Column: '31'\r\n\r\nList Int primes = (1i);primes.Lenght();\r\n                              ^-------\r\n"],
                [declaration6 + "primes,Lenght();", "[Syntactical error]\r\nParser expected 'Semicolon' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '24'\r\n\r\nList Int primes = (1i);primes,Lenght();\r\n                       ^--------------\r\n"],
                [declaration6 + "primesLenght();", "[Runtime error]\r\nFunction 'primesLenght()' is not defined!\r\nFile: 'REPL', Line: '1', Column: '24'\r\n\r\nList Int primes = (1i);primesLenght();\r\n                       ^-------------\r\n"],
                [declaration6 + "prime.Lenght();", "[Runtime error]\r\nVariable 'prime' is not defined!\r\nFile: 'REPL', Line: '1', Column: '24'\r\n\r\nList Int primes = (1i);prime.Lenght();\r\n                       ^-------------\r\n"],
                [declaration6 + "primes.Lenght(1i);", "[Semantical error]\r\nThere is no definition for 'Lenght()' function with 'Int(1)' argument!\r\nFile: 'REPL', Line: '1', Column: '31'\r\n\r\nList Int primes = (1i);primes.Lenght(1i);\r\n                              ^---------\r\n"],
                [declaration6 + "primes.Lenght(Null);", "[Semantical error]\r\nThere is no definition for 'Lenght()' function with 'Null(Null)' argument!\r\nFile: 'REPL', Line: '1', Column: '31'\r\n\r\nList Int primes = (1i);primes.Lenght(Null);\r\n                              ^-----------\r\n"],
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if (result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }

        }

        [Fact]
        public void IndexGetter()
        {
            string declaration1 = "List Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];";
            string declaration2 = "List Float primes = [1.5f,2.5f,3.5f,4.5f,5.5f,6.5f,7.5f,8.5f,9.5f,10.5f];";
            string declaration3 = "List String primes = [\"1i\",\"2i\",\"3i\",\"4i\",\"5i\",\"6i\",\"7i\",\"8i\",\"9i\",\"10i\"];";
            string declaration4 = "List Bool primes = [True, False, True, False, True, False, True, False, True, False];";

            string[][] dataset =
            [
                #region Valid 
                [declaration1 + "primes[0i];", "1"],
                [declaration1 + "primes[1i];", "2"],
                [declaration1 + "primes[2i];", "3"],
                [declaration1 + "primes[3i];", "4"],
                [declaration1 + "primes[4i];", "5"],
                [declaration1 + "primes[5i];", "6"],
                [declaration1 + "primes[6i];", "7"],
                [declaration1 + "primes[7i];", "8"],
                [declaration1 + "primes[8i];", "9"],
                [declaration1 + "primes[9i];", "10"],

                [declaration2 + "primes[0i];", "1.5"],

                [declaration3 + "primes[0i];", "1i"],

                [declaration4 + "primes[0i];", "True"],
                #endregion
                
                #region Invalid 
                [declaration1 + "primes[];", "[Syntactical error]\r\nToken 'RightSquareBracket' was unexpected.\r\nFile: 'REPL', Line: '1', Column: '59'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[];\r\n                                                          ^\r\n"],
                [declaration1 + "primes[-1i];", "[Runtime error]\r\nArgument out of range! Index '-1'\r\nFile: 'REPL', Line: '1', Column: '58'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[-1i];\r\n                                                         ^----\r\n"],
                [declaration1 + "primes[10i];", "[Runtime error]\r\nArgument out of range! Index '10'\r\nFile: 'REPL', Line: '1', Column: '58'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[10i];\r\n                                                         ^----\r\n"],
                [declaration1 + "primes[Null];", "[Semantical error]\r\nInvalid indexer type. Allowed type is 'Int', actual 'Null'\r\nFile: 'REPL', Line: '1', Column: '58'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[Null];\r\n                                                         ^-----\r\n"],
                [declaration1 + "primes[0c];", "[Semantical error]\r\nInvalid indexer type. Allowed type is 'Int', actual 'Char'\r\nFile: 'REPL', Line: '1', Column: '58'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[0c];\r\n                                                         ^---\r\n"],
                [declaration1 + "primes[\"1str\"];", "[Semantical error]\r\nInvalid indexer type. Allowed type is 'Int', actual 'String'\r\nFile: 'REPL', Line: '1', Column: '58'\r\n\r\nList Int primes = [1i,2i,3i,4i,5i,6i,7i,8i,9i,10i];primes[\"1str\"];\r\n                                                         ^-------\r\n"],
                #endregion
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if (result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }
        }

        [Fact]
        public void IndexSetter()
        {
            string declaration1 = "List Int primes = [1i,2i,3i];";

            string[][] dataset =
            [
                #region Valid 
                [declaration1 + "primes[0i];", "1"],
                [declaration1 + "primes[0i] = 4i;", "None"],
                [declaration1 + "primes[0i] = 4i;primes[0i];", "4"],
                [declaration1 + "primes[0i] = 56435i;primes[0i];", "56435"],
                [declaration1 + "primes[0i] = 4i;primes[0i];primes[0i] = 5i;primes[0i];", "5"],
                [declaration1 + "primes[0i] = 4i;primes[0i];primes[0i] = 5i;primes[0i];primes[0i] = 6i;primes[0i];", "6"],

                [declaration1 + "primes[0i] = 4i;primes[0i];", "4"],
                [declaration1 + "primes[1i] = 5i;primes[1i];", "5"],
                [declaration1 + "primes[2i] = 6i;primes[2i];", "6"],
                #endregion

                #region Invalid 
                [declaration1 + "primes[0i] = 4;", "[Lexical error]\r\nToken '4;' is illegal!\r\nFile: 'REPL', Line: '1', Column: '43'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = 4;\r\n                                          ^\r\n"],
                [declaration1 + "primes[0i] = ;", "[Syntactical error]\r\nToken 'Semicolon' was unexpected.\r\nFile: 'REPL', Line: '1', Column: '43'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = ;\r\n                                          ^\r\n"],
                [declaration1 + "primes[0i] =", "[Syntactical error]\r\nToken 'Eof' was unexpected.\r\nFile: 'REPL', Line: '1', Column: '42'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] =\r\n                                         ^\r\n"],
                [declaration1 + "primes[-1i] = 10i;", "[Runtime error]\r\nArgument out of range! Index '-1'\r\nFile: 'REPL', Line: '1', Column: '36'\r\n\r\nList Int primes = [1i,2i,3i];primes[-1i] = 10i;\r\n                                   ^----------\r\n"],
                [declaration1 + "primes[5i] = 10i;", "[Runtime error]\r\nArgument out of range! Index '5'\r\nFile: 'REPL', Line: '1', Column: '36'\r\n\r\nList Int primes = [1i,2i,3i];primes[5i] = 10i;\r\n                                   ^---------\r\n"],
                [declaration1 + "primes[0i] = Null;", "[Semantical error]\r\nCannot assign 'Int(primes) = Null(Null)'\r\nFile: 'REPL', Line: '1', Column: '30'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = Null;\r\n                             ^----------------\r\n"],
                [declaration1 + "primes[0i] = True;", "[Semantical error]\r\nCannot assign 'Int(primes) = Bool(True)'\r\nFile: 'REPL', Line: '1', Column: '30'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = True;\r\n                             ^----------------\r\n"],
                [declaration1 + "primes[0i] = \"Test\";", "[Semantical error]\r\nCannot assign 'Int(primes) = String(\"Test\")'\r\nFile: 'REPL', Line: '1', Column: '30'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = \"Test\";\r\n                             ^------------------\r\n"],
                [declaration1 + "primes[0i] = 3.14f;", "[Semantical error]\r\nCollection is of type 'Int' but provided value is of type 'Float'!\r\nFile: 'REPL', Line: '1', Column: '43'\r\n\r\nList Int primes = [1i,2i,3i];primes[0i] = 3.14f;\r\n                                          ^----\r\n"],
                #endregion
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if (result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }
        }

        [Fact]
        public void RemoveAtMethod()
        {
            string declaration1 = "List Int primes = [1i,2i,3i,4i];";

            string[][] dataset =
            [
                #region Valid
                [declaration1 + "primes.Length();","4"],
                [declaration1 + "primes.RemoveAt(0i);","None"],
                
                [declaration1 + "primes[0i];","1"],
                [declaration1 + "primes.RemoveAt(0i);primes[0i];", "2"],
                [declaration1 + "primes.RemoveAt(0i);primes[0i];primes.RemoveAt(0i);primes[0i];", "3"],
                [declaration1 + "primes.RemoveAt(0i);primes[0i];primes.RemoveAt(0i);primes[0i];primes.RemoveAt(0i);primes[0i];", "4"],

                [declaration1 + "primes.RemoveAt(0i);primes.Length();","3"],
                [declaration1 + "primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();", "2"],
                [declaration1 + "primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();", "1"],
                [declaration1 + "primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();primes.RemoveAt(0i);primes.Length();", "0"],
                #endregion
                
                #region Invalid
                [declaration1 + "primes.RemoveAt();","[Semantical error]\r\nThere is no definition for 'RemoveAt()' function!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt();\r\n                                       ^---------\r\n"],
                [declaration1 + "primes.RemoveAt(-1i);","[Runtime error]\r\nArgument out of range! Index '-1'\r\nFile: 'REPL', Line: '1', Column: '50'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt(-1i);\r\n                                                 ^--\r\n"],
                [declaration1 + "primes.RemoveAt(4i);","[Runtime error]\r\nArgument out of range! Index '4'\r\nFile: 'REPL', Line: '1', Column: '49'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt(4i);\r\n                                                ^--\r\n"],
                [declaration1 + "primes.RemoveAt(5i);","[Runtime error]\r\nArgument out of range! Index '5'\r\nFile: 'REPL', Line: '1', Column: '49'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt(5i);\r\n                                                ^--\r\n"],
                [declaration1 + "primes.RemoveAt(Null);","[Semantical error]\r\nThere is no definition for 'RemoveAt()' function with 'Null(Null)' argument!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt(Null);\r\n                                       ^-------------\r\n"],
                [declaration1 + "primes.RemoveAt(\"1i\");","[Semantical error]\r\nThere is no definition for 'RemoveAt()' function with 'String(1i)' argument!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.RemoveAt(\"1i\");\r\n                                       ^-------------\r\n"],
                #endregion
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if (result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }
        }

        [Fact]
        public void ClearMethod()
        {
            string declaration1 = "List Int primes = [1i,2i,3i,4i];";

            string[][] dataset =
            [
                #region Valid
                [declaration1 + "primes.Clear();","None"],
                [declaration1 + "primes.Clear();primes.Length();","0"],
                [declaration1 + "primes.Clear();primes.Length();primes.Add(1i);","None"],
                [declaration1 + "primes.Clear();primes.Length();primes.Add(1i);primes.Length();","1"],
                [declaration1 + "primes.Clear();primes.Length();primes.Add(1i);primes.Length();primes.Clear();","None"],
                [declaration1 + "primes.Clear();primes.Length();primes.Add(1i);primes.Length();primes.Clear();primes.Length();","0"],
                #endregion
                
                #region Invalid
                ["primes.Clear();","[Runtime error]\r\nVariable 'primes' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nprimes.Clear();\r\n^-------------\r\n"],
                [declaration1 + "primes.Clear(1i, 1i);","[Semantical error]\r\nThere is no definition for 'Clear()' function with 'Int(1), Int(1)' arguments!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Clear(1i, 1i);\r\n                                       ^------------\r\n"],
                [declaration1 + "primes.Claer();","[Semantical error]\r\nThere is no definition for 'Claer()' function!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Claer();\r\n                                       ^------\r\n"],
                [declaration1 + "primess.Clear();","[Runtime error]\r\nVariable 'primess' is not defined!\r\nFile: 'REPL', Line: '1', Column: '33'\r\n\r\nList Int primes = [1i,2i,3i,4i];primess.Clear();\r\n                                ^--------------\r\n"],
                #endregion
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if (result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }
        }

        [Fact]
        public void AddMethod()
        {
            string declaration1 = "List Int primes = [1i,2i,3i,4i];";
            string declaration2 = "List Int primes = [];";

            string declaration3 = "List Float primes = [10.0f];";

            string declaration4 = "List String primes = [\"Name\"];";

            string[][] dataset =
            [
                #region General calls
                [declaration1 + "primes.Add;","[Syntactical error]\r\nParser expected 'LeftParenthesis' token but actual token was 'Semicolon'.\r\nFile: 'REPL', Line: '1', Column: '43'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Add;\r\n                                          ^\r\n"],
                [declaration1 + "primes.Add(1i,2i);","[Semantical error]\r\nThere is no definition for 'Add()' function with 'Int(1), Int(2)' arguments!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Add(1i,2i);\r\n                                       ^---------\r\n"],
                [declaration1 + "primes.Add(a,b);","[Runtime error]\r\nVariable 'a' is not defined!\r\nFile: 'REPL', Line: '1', Column: '44'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Add(a,b);\r\n                                           ^---\r\n"],
                [declaration1 + "primes.Add();","[Semantical error]\r\nThere is no definition for 'Add()' function!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Int primes = [1i,2i,3i,4i];primes.Add();\r\n                                       ^----\r\n"],
                ["primes.Add();","[Runtime error]\r\nVariable 'primes' is not defined!\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nprimes.Add();\r\n^-----------\r\n"],
                #endregion

                #region Declaration 1/2 - Collection of 'Int'
                [declaration1 + "primes.Length();", "4"],
                [declaration1 + "primes.Add(5i);", "None"],
                [declaration1 + "primes.Add(5i + 15i);primes.Length();", "5"],
                [declaration1 + "primes.Add(5i);primes.Length();", "5"],
                [declaration1 + "primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Length();", "8"],
                [declaration1 + "primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Length();", "10"],
                [declaration1 + "primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Length();primes.Add(5i);", "None"],
                
                [declaration2 + "primes.Length();", "0"],
                [declaration2 + "primes.Add(5i);", "None"],
                [declaration2 + "primes.Add(5i);primes.Length();", "1"],
                [declaration2 + "primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Length();primes.Add(5i);primes.Add(5i);", "None"],
                [declaration2 + "primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Add(5i);primes.Length();primes.Add(5i);primes.Add(5i);primes.Length();", "6"],
                #endregion

                #region Declaration 3 - Collection of 'Float'
                [declaration3 + "primes.Length();", "1"],
                [declaration3 + "primes.Add(5i);", "[Semantical error]\r\nCollection is of type 'Float' but provided value is of type 'Int'!\r\nFile: 'REPL', Line: '1', Column: '40'\r\n\r\nList Float primes = [10.0f];primes.Add(5i);\r\n                                       ^--\r\n"],
                #endregion

                #region Declaration 4 - Collection of 'String'
                [declaration4 + "primes.Length();", "1"],
                [declaration4 + "primes.Add(\"Name\");", "None"],
                [declaration4 + "primes.Add(\"Name\");primes.Length();", "2"],
                #endregion
            ];

            foreach (string[] set in dataset)
            {
                string code = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(code);
                string STR = result.ToString();

                if (STR != expected)
                {
                    if(result is ErrorObject asError)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        Assert.Fail($"'{code}' returned '{STR}' and should '{expected}'");
                    }
                }
            }
        }

        [Fact]
        public void Declaration()
        {
            string[] code = new string[]
            {
                "List Int primes = [2i, 3i, 5i, 7i, 9i];",
                "List Float primes = [2i, 1.5f, \"John\"];",
                "List String primes = [\"John\"];",
                "List Float pies = [1.1f, 2.2f, 3.3f, 4.4f, 5.5f];",
                "List String names = [\"Mark\", \"Adam\", \"Jordan\", \"David\", \"Isaac\"];",
                "List Int temp = (0i);",
                "List Int temp = (1i);",
                "List Float temp = (10i);",
                "List Int temp = [];",
                "List Int temp = [1i];",
            };

            string[][] isOutcomeValid = new string[][]
            {
                ["true", "5", "IntObject", "true", "0"],
                ["false", "3", "FloatObject", "true", "0"],
                ["true", "1", "StringObject", "true", ""],
                ["true", "5", "FloatObject", "true", "0"],
                ["true", "5", "StringObject", "true", ""],
                ["true", "0", "IntObject", "false", "0"],
                ["true", "1", "IntObject", "false", "0"],
                ["true", "10", "FloatObject", "false", "0"],
                ["true", "0", "IntObject", "true", "0"],
                ["true", "1", "IntObject", "true", "0"],
            };

            string[] results = new string[] { };
            string statement = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                statement = code[i];
                results = isOutcomeValid[i];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.Evaluate(statement);
                if (results[0] == "false")
                {
                    Assert.True(result is ErrorObject);
                }
                else
                {
                    Assert.True(result is not ErrorObject);

                    //  Check type
                    Assert.Equal(results[2], result.Type.Name);

                    //  Check count
                    Assert.Equal(results[1], (result as IObjectCollection).Collection.Count.ToString());

                    //  Check collection's item's types
                    IObjectCollection collection = result as IObjectCollection;

                    if (results[3] == "true")
                    {
                        for(int j = 0; j < collection.Collection.Count; j++)
                        {
                            IObject item = collection.Collection[j];

                            Assert.Equal(results[2], item.Type.Name);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < collection.Collection.Count; j++)
                        {
                            IObject item = collection.Collection[j];

                            if(collection is ObjectCollection asIntColl)
                            {
                                Type actualItemType = collection.Type;

                                if(actualItemType.Name == "IntObject")
                                {
                                    Assert.Equal(results[4], (item as IntObject).Value.ToString());
                                }
                                else if (actualItemType.Name == "StringObject")
                                {
                                    Assert.Equal(results[4], (item as StringObject).Value);
                                }
                                else if (actualItemType.Name == "FloatObject")
                                {
                                    Assert.Equal(results[4], (item as FloatObject).Value.ToString());
                                }
                                else
                                {
                                    Assert.Fail("TODO: Type not supported!");
                                }
                            }
                            else
                            {
                                Assert.Fail("Not defined collection!");
                            }
                        }
                    }
                }
            }
        }
    }
}