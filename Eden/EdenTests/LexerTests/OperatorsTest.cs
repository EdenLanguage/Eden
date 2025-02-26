using EdenClasslibrary.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenTests.LexerTests
{
    public class OperatorsTest
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
    }
}
