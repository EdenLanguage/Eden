using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class FloatTest
    {

        [Fact]
        public void ParseFloat_1()
        {
            string code = $"3.14;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Float, "3.14"),
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
        public void ParseFloat_2()
        {
            string code = $"34224.2424442;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Float, "34224.2424442"),
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
        public void ParseFloat_3()
        {
            string code = $"34224.;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Float, "34224"),
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
        public void ParseFloat_4()
        {
            string code = $"0.0009;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Float, "0.0009"),
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
        public void ParseFloat_5()
        {
            string code = $"dssd0.0009dssd;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Identifier, "dssd"),
                new Token(TokenType.Float, "0.0009"),
                new Token(TokenType.Identifier, "dssd"),
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
        public void ParseFloat_6()
        {
            string code = $"dssd0.0009.dssd;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Identifier, "dssd"),
                new Token(TokenType.Float, "0.0009"),
                new Token(TokenType.Dot, "."),
                new Token(TokenType.Identifier, "dssd"),
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
        public void ParseFloat_7()
        {
            string code = $"Var Float pi = 3.14;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Float"),
                new Token(TokenType.Identifier, "pi"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Float, "3.14"),
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
