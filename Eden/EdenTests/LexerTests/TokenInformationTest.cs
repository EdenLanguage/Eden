using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class TokenInformationTest
    {
        [Fact]
        public void TokensDetails_1()
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
    }
}
