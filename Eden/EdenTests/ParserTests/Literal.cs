using EdenTests.Utility;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.AbstractSyntaxTree.Statements;
using Bogus;

namespace EdenTests.ParserTests
{
    public class Literal : FileTester
    {
        string[][] ValidFilesTestSet =
        [
            [GetLiteralsSourceFile("literalUsage1.eden"),"None"],
            [GetLiteralsSourceFile("literalUsage2.eden"),"None"],
            [GetLiteralsSourceFile("literalUsage3.eden"),"None"],
            [GetLiteralsSourceFile("simpleLiteralsDefinitions.eden"),"None"],
            [GetLiteralsSourceFile("complexLiteralDefinitions.eden"),"None"],
        ];

        string[][] InvalidFilesTestSet =
        [
            [GetLiteralsSourceFile("illegalLiteralDefinition.eden"),"[Semantical error]\r\nStatement 'Literal' cannot be used in Top-Level statements scope!\r\nFile: 'illegalLiteralDefinition.eden', Line: '2', Column: '5'\r\n\r\n    Literal 10i As #integer;\r\n    ^-----------------------"],
        ];

        [Fact]
        public void Valid()
        {
            string[][] data =
            [
                ["Literal 10i As #Count;", ""],
                ["Literal 3.14f As #Pi;", ""],
                ["Literal 10c As #Symbol;", ""],
                ["Literal \"Pablo\" As #Name;", ""],
                ["Literal True As #Flag;", ""],
            ];


            for (int i = 0; i < data.Length; i++)
            {
                string input = data[i][0];
                string expected = data[i][1];

                Parser parser = new Parser();
                Statement statament = parser.Parse(input);

                string STR = statament.ToString();

                if (statament is InvalidStatement asInvalidStmt)
                {
                    Assert.Fail(asInvalidStmt.Print());
                }
            }
        }

        [Fact]
        public void Invalid()
        {
            string[][] data =
            [
                ["Literal 10i asdasd As #Count;", "[Syntactical error]\r\nParser expected 'As' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '13'\r\n\r\nLiteral 10i asdasd As #Count;\r\n            ^----------------"],
                ["Literal 3.14f as #Pi;", "[Syntactical error]\r\nParser expected 'As' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '15'\r\n\r\nLiteral 3.14f as #Pi;\r\n              ^------"],
                ["literal 10c As #Symbol;", "[Syntactical error]\r\nParser expected 'Semicolon' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '1'\r\n\r\nliteral 10c As #Symbol;\r\n^----------------------"],
                ["Literal \"Pablo\" #As Name", "[Syntactical error]\r\nParser expected 'As' token but actual token was 'LiteralIdentifier'.\r\nFile: 'REPL', Line: '1', Column: '17'\r\n\r\nLiteral \"Pablo\" #As Name\r\n                ^-------"],
                ["Literal True Ass #Flag;", "[Syntactical error]\r\nParser expected 'As' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '14'\r\n\r\nLiteral True Ass #Flag;\r\n             ^---------"],
                ["Literal True #Flag;", "[Syntactical error]\r\nParser expected 'As' token but actual token was 'LiteralIdentifier'.\r\nFile: 'REPL', Line: '1', Column: '14'\r\n\r\nLiteral True #Flag;\r\n             ^-----"],
                ["Literal As ;", "[Semantical error]\r\nCannot create definition of Literal with value 'As' because it cannot be replaced with Literal value!\r\nFile: 'REPL', Line: '1', Column: '9'\r\n\r\nLiteral As ;\r\n        ^---"],
                ["Literal 10i As name;", "[Syntactical error]\r\nParser expected 'LiteralIdentifier' token but actual token was 'Identifier'.\r\nFile: 'REPL', Line: '1', Column: '16'\r\n\r\nLiteral 10i As name;\r\n               ^----"],
                ["Literal 10i As #", "[Lexical error]\r\nToken '#' is illegal!\r\nFile: 'REPL', Line: '1', Column: '16'\r\n\r\nLiteral 10i As #\r\n               ^"],
            ];

            for (int i = 0; i < data.Length; i++)
            {
                string input = data[i][0];
                string expected = data[i][1];

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    expected = data[i][1].Replace("\r", "");
                }

                Parser parser = new Parser();
                Statement ast = parser.Parse(input);

                string STR = ast.ToString();

                if (STR != expected)
                {
                    if (ast is not InvalidStatement)
                    {
                        Assert.Fail($"'{input}' should fail but it didn't!");
                    }
                    else
                    {
                        Assert.Fail(STR);
                    }
                }

            }
        }

        [Fact]
        public void ValidFiles_Parsing()
        {
            foreach (string[] set in ValidFilesTestSet)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Statement ast = parser.ParseFile(input);

                string STR = ast.ToAbstractSyntaxTree();

                if (ast is InvalidStatement AsInvalidStatement)
                {
                    Assert.Fail($"{AsInvalidStatement.Print()}");
                }
            }
        }

        [Fact]
        public void ValidFiles_Evaluation()
        {
            foreach (string[] set in ValidFilesTestSet)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (expected != STR)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        string fileName = Path.GetFileName(input) + " -> ";
                        Assert.Equal(fileName + expected, fileName + STR);
                    }
                }
            }
        }

        [Fact]
        public void InvalidFiles_Evaluation()
        {
            foreach (string[] set in InvalidFilesTestSet)
            {
                string input = set[0];
                string expected = set[1];

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                IObject result = evaluator.EvaluateFile(input);
                string STR = result.ToString();

                if (expected != STR)
                {
                    if (result is ErrorObject)
                    {
                        Assert.Fail(STR);
                    }
                    else
                    {
                        string fileName = Path.GetFileName(input) + " -> ";
                        Assert.Equal(fileName + expected, fileName + STR);
                    }
                }
            }
        }

        [Fact]
        public void InvalidFiles_Parsing()
        {
            foreach (string[] set in InvalidFilesTestSet)
            {
                string input = set[0];
                string expected = set[1];

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    expected = set[1].Replace("\r", "");
                }

                Parser parser = new Parser();
                Evaluator evaluator = new Evaluator(parser);

                Statement ast = parser.ParseFile(input);

                string STR = ast.ToString();

                if (expected != STR)
                {
                    if (ast is not InvalidStatement)
                    {
                        Assert.Fail($"'{input}' should fail but it didn't!");
                    }
                    else
                    {
                        Assert.Fail(STR);
                    }
                }
            }
        }

        [Fact]
        public void SimpleLiteralsConstruction()
        {
            string filename = "simpleLiteralsDefinitions.eden";
            string executionLocation = GetLiteralsSourceFile(filename);

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            if (ast is InvalidStatement asInvalidStatement)
            {
                Assert.Fail(asInvalidStatement.Print());
            }

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();
        }

        [Fact]
        public void ComplexLiteralsConstruction()
        {
            string filename = "complexLiteralDefinitions.eden";
            string executionLocation = GetLiteralsSourceFile(filename);

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            if (ast is InvalidStatement asInvalidStatement)
            {
                Assert.Fail(asInvalidStatement.Print());
            }

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();
        }

        [Fact]
        public void IllegalLiteralsConstruction()
        {
            string filename = "illegalLiteral.eden";
            string executionLocation = GetLiteralsSourceFile(filename);

            Parser parser = new Parser();
            Statement ast = parser.ParseFile(executionLocation);

            string AST = ast.ToAbstractSyntaxTree();
            string STR = ast.ToString();

            if (ast is not InvalidStatement)
            {
                Assert.Fail(STR);
            }
        }
    }
}
