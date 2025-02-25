using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class Int
    {

        [Fact]
        public void ParseInt_1()
        {
            string code = $"202020;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Int, "202020"),
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
        public void ParseInt_2()
        {
            string code = $"0;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Int, "0"),
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
        public void ParseInt_3()
        {
            string code = $"0a;";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Int, "0"),
                new Token(TokenType.Identifier, "a"),
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
