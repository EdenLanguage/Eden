using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class KeywordsTest
    {
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
    }
}
