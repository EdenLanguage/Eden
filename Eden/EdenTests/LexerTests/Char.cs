using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Char : FileTester
    {
        [Fact]
        public void Char1()
        {
            Lexer lexer = new Lexer();

            string filename = "char1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                new Token(keyword: TokenType.Char, value: "'a'", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'b'", line: 2, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'c'", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "' '", line: 4, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'1'", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'9'", line: 6, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'\\0'", line: 7, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'\\t'", line: 8, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'\\r'", line: 9, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "'\\n'", line: 10, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "100c", line: 11, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "0c", line: 12, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Char, value: "255c", line: 13, startPos: 1, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 13, startPos: 5, filename: filename),
            ];

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