using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class StringTest
    {

        [Fact]
        public void ParseString_1()
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
