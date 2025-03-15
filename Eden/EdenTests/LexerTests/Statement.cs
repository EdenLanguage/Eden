using EdenClasslibrary.Types;
using EdenTests.Utility;
using System.Text;

namespace EdenTests.LexerTests
{
    public class Statement : FileTester
    {
        [Fact]
        public void Statements1()
        {
            Lexer lexer = new Lexer();

            string filename = "statements1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                //  First statement
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 1, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "zmienna", line: 1, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 1, startPos: 17, filename: filename),
                new Token(keyword: TokenType.Int, value: "5i", line: 1, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 21, filename: filename),

                //  Second statement
                new Token(keyword: TokenType.Function, value: "Function", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 3, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "Calculator", line: 3, startPos: 14, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 3, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Var, value: "Var", line: 3, startPos: 25, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 3, startPos: 29, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 3, startPos: 33, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 3, startPos: 34, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 3, startPos: 35, filename: filename),
                new Token(keyword: TokenType.Return, value: "Return", line: 4, startPos: 2, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 4, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 4, startPos: 10, filename: filename),
                new Token(keyword: TokenType.RightBracket, value: "}", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 5, startPos: 2, filename: filename),

                //  Third statement
                new Token(keyword: TokenType.Var, value: "Var", line: 7, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 7, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "result", line: 7, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 7, startPos: 16, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "Calculator", line: 7, startPos: 18, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 7, startPos: 28, filename: filename),
                new Token(keyword: TokenType.Int, value: "5i", line: 7, startPos: 29, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 7, startPos: 31, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 7, startPos: 32, filename: filename),

                //  Third statement
                new Token(keyword: TokenType.If, value: "If", line: 9, startPos: 1, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 9, startPos: 3, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "counter", line: 9, startPos: 4, filename: filename),
                new Token(keyword: TokenType.Equal, value: "==", line: 9, startPos: 12, filename: filename),
                new Token(keyword: TokenType.Int, value: "5i", line: 9, startPos: 15, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 9, startPos: 17, filename: filename),
                new Token(keyword: TokenType.Return, value: "Return", line: 9, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Int, value: "10i", line: 9, startPos: 26, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 9, startPos: 29, filename: filename),

                //  Forth statement
                new Token(keyword: TokenType.Else, value: "Else", line: 11, startPos: 1, filename: filename),
                new Token(keyword: TokenType.If, value: "If", line: 11, startPos: 6, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 11, startPos: 8, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "counter", line: 11, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Equal, value: "==", line: 11, startPos: 17, filename: filename),
                new Token(keyword: TokenType.Bool, value: "True", line: 11, startPos: 20, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 11, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Return, value: "Return", line: 11, startPos: 26, filename: filename),
                new Token(keyword: TokenType.Int, value: "20i", line: 11, startPos: 33, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 11, startPos: 36, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 11, startPos: 37, filename: filename),
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

        [Fact]
        public void Statements2()
        {
            Lexer lexer = new Lexer();

            string filename = "statements2.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                //  First statement
                new Token(keyword: TokenType.Int, value: "5i", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Equal, value: "==", line: 1, startPos: 4, filename: filename),
                new Token(keyword: TokenType.Int, value: "5i", line: 1, startPos: 7, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 1, startPos: 9, filename: filename),
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
