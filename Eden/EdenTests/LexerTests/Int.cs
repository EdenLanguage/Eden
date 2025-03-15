using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Int : FileTester
    {
        [Fact]
        public void Int1()
        {
            Lexer lexer = new Lexer();

            string filename = "int1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Int, value: "0i", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 3, filename: filename),

                // Second line
                new Token(keyword: TokenType.Int, value: "1i", line: 2, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 2, startPos: 3, filename: filename),

                // Third line
                new Token(keyword: TokenType.Int, value: "10i", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 3, startPos: 4, filename: filename),

                // Forth line
                new Token(keyword: TokenType.Int, value: "100i", line: 4, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 4, startPos: 5, filename: filename),

                // Fifth line
                new Token(keyword: TokenType.Int, value: "1000i", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 5, startPos: 6, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 5, startPos: 7, filename: filename),
            ];

            Assert.Equal(expected.Length, actual.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                bool isSame = actualToken.Equals(expectedToken);

                if (isSame == false)
                {
                    Assert.Fail($"Tokens at position '{i}' are different!");
                }
            }
        }
    }
}
