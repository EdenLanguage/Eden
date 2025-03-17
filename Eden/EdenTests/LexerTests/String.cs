using EdenClasslibrary.Types;
using EdenTests.Utility;
using System.Text;

namespace EdenTests.LexerTests
{
    public class String : FileTester
    {
        [Fact]
        public void String1()
        {
            Lexer lexer = new Lexer();

            string filename = "string1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                //  First statement
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "String", line: 1, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "name", line: 1, startPos: 12, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 1, startPos: 17, filename: filename),
                new Token(keyword: TokenType.String, value: "\"Jaroslaw\"", line: 1, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 29, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 1, startPos: 30, filename: filename),
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
        public void StringInvalid1()
        {
            Lexer lexer = new Lexer();

            string filename = "stringInvalid1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                new Token(keyword: TokenType.Illegal, value: "\"sdasd", line: 1, startPos: 1, filename: filename),
            ];

            for (int i = 0; i < expected.Length; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                bool isSame = actualToken.Keyword == expectedToken.Keyword;

                if (isSame == false)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine($"Tokens at position '{i}' are different!");

                    Assert.Fail(sb.ToString());
                }
            }
        }
    }
}
