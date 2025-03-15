using EdenClasslibrary.Types;
using EdenTests.Utility;
using System.Text;

namespace EdenTests.LexerTests
{
    public class OperatorsTest : FileTester
    {
        [Fact]
        public void Operators1()
        {
            Lexer lexer = new Lexer();

            string filename = "operators1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                //  First line
                new Token(keyword: TokenType.Plus, value: "+", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 1, startPos: 2, filename: filename),
                new Token(keyword: TokenType.RightBracket, value: "}", line: 1, startPos: 3, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 1, startPos: 4, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 1, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 1, startPos: 6, filename: filename),
                new Token(keyword: TokenType.Plus, value: "+", line: 1, startPos: 7, filename: filename),
                new Token(keyword: TokenType.Minus, value: "-", line: 1, startPos: 8, filename: filename),

                //  Second line
                new Token(keyword: TokenType.Plus, value: "+", line: 2, startPos: 1, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 2, startPos: 2, filename: filename),
                new Token(keyword: TokenType.RightBracket, value: "}", line: 2, startPos: 3, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 2, startPos: 6, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 2, startPos: 7, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 2, startPos: 8, filename: filename),
                new Token(keyword: TokenType.Plus, value: "+", line: 2, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Minus, value: "-", line: 2, startPos: 12, filename: filename),

                new Token(keyword: TokenType.LesserOrEqual, value: "<=", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.GreaterOrEqual, value: ">=", line: 4, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Inequal, value: "!=", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Equal, value: "==", line: 6, startPos: 1, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 7, startPos: 1, filename: filename),
            ];

            for (int i = 0; i < expected.Length; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                bool isSame = actualToken.Equals(expectedToken);

                if (isSame == false)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Tokens at position '{i}' are different!");
                    sb.AppendLine(PrintTokenDiff(actualToken, expectedToken));

                    Assert.Fail(sb.ToString());
                }
            }
        }
    }
}
