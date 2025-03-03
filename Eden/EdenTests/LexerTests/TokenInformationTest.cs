using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class TokenInformationTest : FileTester
    {
        [Fact]
        public void TokensDetails_1()
        {
            string filename = "main9.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Lexer lexer = new Lexer();
            lexer.LoadFile(executionLocation);

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(TokenType.Keyword, "Var", line: 1, startPos: 1, filename: executionLocation),
                new Token(TokenType.VariableType, "Int", line: 1, startPos: 5, filename: executionLocation),
                new Token(TokenType.Identifier, "const", line: 1, startPos: 9, filename: executionLocation),
                new Token(TokenType.Assign, "=", line: 1, startPos: 15, filename: executionLocation),
                new Token(TokenType.Int, "5050", line: 1, startPos: 17, filename: executionLocation),
                new Token(TokenType.Semicolon, ";", line: 1, startPos: 21, filename: executionLocation),

                // Second line
                new Token(TokenType.Keyword, "Var", line: 2, startPos: 1, filename: executionLocation),
                new Token(TokenType.VariableType, "Bool", line: 2, startPos: 5, filename: executionLocation),
                new Token(TokenType.Identifier, "flag", line: 2, startPos: 10, filename: executionLocation),
                new Token(TokenType.Assign, "=", line: 2, startPos: 15, filename: executionLocation),
                new Token(TokenType.Bool, "False", line: 2, startPos: 17, filename: executionLocation),
                new Token(TokenType.Semicolon, ";", line: 2, startPos: 22, filename: executionLocation),
                new Token(TokenType.Eof, "\0", line: 2, startPos: 23, filename: executionLocation),
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
