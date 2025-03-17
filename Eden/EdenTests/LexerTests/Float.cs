using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Float : FileTester
    {

        [Fact]
        public void Float1()
        {
            Lexer lexer = new Lexer();

            string filename = "float1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                new Token(keyword: TokenType.Float, value: "3.14f", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Float, value: "34224.2424442f", line: 2, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Float, value: "0.0009f", line: 3, startPos: 1, filename: filename),

                new Token(keyword: TokenType.Var, value: "Var", line: 4, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Float", line: 4, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "pi", line: 4, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 4, startPos: 14, filename: filename),
                new Token(keyword: TokenType.Float, value: "3.14f", line: 4, startPos: 16, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 4, startPos: 21, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 4, startPos: 22, filename: filename),
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
        public void FloatIllegal1()
        {
            Lexer lexer = new Lexer();

            string filename = "floatIllegarl1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                new Token(keyword: TokenType.Identifier, value: "dssd", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Illegal, value: "0.0009d", line: 1, startPos: 5, filename: filename),
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