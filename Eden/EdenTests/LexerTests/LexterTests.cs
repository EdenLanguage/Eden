using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class LexterTests
    {
        [Fact]
        public void BasicTokens()
        {
            string code = "+{}()=+-;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expectedTokens = new List<Token>()
            {
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Minus, "-"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actualTokens = lexer.Tokenize().ToList();

            Assert.Equal(expectedTokens.Count, actualTokens.Count);
            for (int i = 0; i < expectedTokens.Count; i++)
            {
                Token expectedToken = expectedTokens[i];
                Token actualToken = actualTokens[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void BasicTokensWhiteSpace()
        {
            string code = "+{}  ()=+  -;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Minus, "-"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void VariableAssingment()
        {
            string code = "Var Int zmienna = 5;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "zmienna"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Int, "5"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void FunctionDefinition()
        {
            string code = "Function Int Calculator(Var Int A){\r\n\tReturn A\r\n};";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Function"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "Calculator"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void FunctionCall()
        {
            string code = "Var Int result = Calculator(5);";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "result"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Identifier, "Calculator"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Int, "5"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void Equal()
        {
            string code = "counter == 5";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Identifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Int, "5"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void IfStatement()
        {
            string code = "If(counter == 5) Return 10;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "If"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Identifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Int, "5"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Int, "10"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void ElseIfStatement()
        {
            string code = "Else If(counter == True) Return 20;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);
            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Else"),
                new Token(TokenType.Keyword, "If"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Identifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Bool, "True"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Int, "20"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void GreaterLesser()
        {
            string code = "If(variable <= 0)";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "If"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Identifier, "variable"),
                new Token(TokenType.LesserOrEqual, "<="),
                new Token(TokenType.Int, "0"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }

        [Fact]
        public void TokenInformation()
        {
            string code = $"Var Int const = 5050;{Environment.NewLine}Var Bool flag = False;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(TokenType.Keyword, "Var", line: 0, startPos: 0),
                new Token(TokenType.VariableType, "Int", line: 0, startPos: 4),
                new Token(TokenType.Identifier, "const", line: 0, startPos: 8),
                new Token(TokenType.Assign, "=", line: 0, startPos: 14),
                new Token(TokenType.Int, "5050", line: 0, startPos: 16),
                new Token(TokenType.Semicolon, ";", line: 0, startPos: 20),

                // Second line
                new Token(TokenType.Keyword, "Var", line: 1, startPos: 0),
                new Token(TokenType.VariableType, "Bool", line: 1, startPos: 4),
                new Token(TokenType.Identifier, "flag", line: 1, startPos: 9),
                new Token(TokenType.Assign, "=", line: 1, startPos: 14),
                new Token(TokenType.Bool, "False", line: 1, startPos: 16),
                new Token(TokenType.Semicolon, ";", line: 1, startPos: 21),
                new Token(TokenType.Eof, "\0", line: 1, startPos: 22),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                bool areEqual = actualToken.Equals(expectedToken);
                Assert.True(areEqual);
            }
        }

        [Fact]
        public void String_ParseString_1()
        {
            string code = $"Var String name = \"Jaroslaw\";";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "String"),
                new Token(TokenType.Identifier, "name"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.String, "\"Jaroslaw\""),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }
    }
}
