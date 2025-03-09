using EdenClasslibrary.Types;
using EdenTests.Utility;

namespace EdenTests.LexerTests
{
    public class FileTest : FileTester
    {
        #region From '*.eden' file
        [Fact]
        public void ParseFile_1()
        {
            Lexer lexer = new Lexer();

            string filename = "main1.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            lexer.LoadFile(executionLocation);

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Bool"),
                new Token(TokenType.Identifier, "flaga"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Bool, "False"),
                new Token(TokenType.Semicolon, ";"),

                // Second line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "counter"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Int, "1024"),
                new Token(TokenType.Semicolon, ";"),
                
                // Third line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Float"),
                new Token(TokenType.Identifier, "pi"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Float, "3.14"),
                new Token(TokenType.Semicolon, ";"),

                // Forth line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "String"),
                new Token(TokenType.Identifier, "name"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.String, "\"Jaworek\""),
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
        public void ParseFile_2()
        {
            Lexer lexer = new Lexer();

            string filename = "main2.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Console.WriteLine($"Tokenized tokens: '{actual.Count}'");

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(TokenType.Function, "Function"),
                new Token(TokenType.VariableType, "Bool"),
                new Token(TokenType.Identifier, "IsGreater"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Comma, ","),
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "B"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),

                // Second line
                new Token(TokenType.If, "If"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.RightArrow, ">"),
                new Token(TokenType.Identifier, "B"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),
                
                // Third line
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Bool, "True"),
                new Token(TokenType.Semicolon, ";"),
                
                //  Forth line
                new Token(TokenType.RightBracket, "}"),

                //  Fifth line
                new Token(TokenType.Else, "Else"),
                new Token(TokenType.LeftBracket, "{"),
                
                //  Sixth line
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Bool, "False"),
                new Token(TokenType.Semicolon, ";"),
                
                // Seventh line
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),

                //  Eight Line
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),

                new Token(TokenType.Eof, "\0"),
            };

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
        public void ParseFile_3()
        {
            Lexer lexer = new Lexer();

            string filename = "main3.eden";
            string executionLocation = Path.Combine(GetTestFilesDirectory(), filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Console.WriteLine($"Tokenized tokens: '{actual.Count}'");

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(TokenType.Function, "Function"),
                new Token(TokenType.VariableType, "Bool"),
                new Token(TokenType.Identifier, "PowOfTwo"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),

                // Second line
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Star, "*"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Semicolon, ";"),

                // Third line
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),

                // Forth line
                new Token(TokenType.Function, "Function"),
                new Token(TokenType.VariableType, "Bool"),
                new Token(TokenType.Identifier, "PowOfThree"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),

                // Fifth line
                new Token(TokenType.Keyword, "Return"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Star, "*"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Star, "*"),
                new Token(TokenType.Identifier, "A"),
                new Token(TokenType.Semicolon, ";"),

                // Sixth line
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),

                // Seventh line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "x"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Identifier, "PowOfTwo"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Int, "2"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Semicolon, ";"),

                // Eight line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "y"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Identifier, "PowOfThree"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Int, "2"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Semicolon, ";"),

                // Tenth line
                new Token(TokenType.Keyword, "Var"),
                new Token(TokenType.VariableType, "Int"),
                new Token(TokenType.Identifier, "sum"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Identifier, "x"),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Identifier, "y"),
                new Token(TokenType.Semicolon, ";"),

                new Token(TokenType.Eof, "\0"),
            };

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.LiteralValue, actualToken.LiteralValue);
            }
        }
        #endregion
    }
}
