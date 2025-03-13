using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class Comments : FileTester
    {
        [Fact]
        public void SimpleComment()
        {
            string code = $"20i;//c\n" +
                $"//c";
            Lexer lexer = new Lexer();
            lexer.SetInput(code);

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Int, "20"),
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

        [Fact]
        public void FromFile_1()
        {
            string filename = "main26.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Lexer lexer = new Lexer();
            lexer.LoadFile(executionLocation);

            List<Token> actual = lexer.Tokenize().ToList();
        }

        [Fact]
        public void FromFile_2()
        {
            string filename = "main27.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            Lexer lexer = new Lexer();
            lexer.LoadFile(executionLocation);

            List<Token> actual = lexer.Tokenize().ToList();
        }
    }
}
