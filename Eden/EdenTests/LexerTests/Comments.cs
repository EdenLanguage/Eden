using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Comments : FileTester
    {
        [Fact]
        public void Comment1()
        {
            Lexer lexer = new Lexer();

            string filename = "comment1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Int, value: "5i", line: 1, startPos: 6, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 8, filename: filename),

                // Second line

                // Third line

                // Forth line
                new Token(keyword: TokenType.Int, value: "5i", line: 4, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 4, startPos: 3, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 5, startPos: 4, filename: filename),
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

        [Fact]
        public void Comment2()
        {
            Lexer lexer = new Lexer();

            string filename = "comment2.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Char", line: 1, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "identifier", line: 1, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 1, startPos: 35, filename: filename),
                new Token(keyword: TokenType.Char, value: "'0'", line: 1, startPos: 51, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 54, filename: filename),

                // Second line
                new Token(keyword: TokenType.Var, value: "Var", line: 2, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Char", line: 2, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "val", line: 2, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 2, startPos: 14, filename: filename),
                new Token(keyword: TokenType.Char, value: "100c", line: 2, startPos: 16, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 2, startPos: 20, filename: filename),

                // Third line
                new Token(keyword: TokenType.Var, value: "Var", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Char", line: 3, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "vals", line: 3, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 3, startPos: 29, filename: filename),
                new Token(keyword: TokenType.Char, value: "0c", line: 3, startPos: 31, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 3, startPos: 33, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 3, startPos: 34, filename: filename),
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

        [Fact]
        public void Comment3()
        {
            Lexer lexer = new Lexer();

            string filename = "comment3.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Int, value: "20i", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 4, filename: filename),
                
                // Second line
                new Token(keyword: TokenType.Eof, value: "\0", line: 2, startPos: 4, filename: filename),
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
