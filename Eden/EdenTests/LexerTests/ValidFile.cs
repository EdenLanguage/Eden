using EdenClasslibrary.Types;
using EdenTests.Utility;
using System.Text;

namespace EdenTests.LexerTests
{
    public class ValidFile : FileTester
    {
        [Fact]
        public void Lex1()
        {
            Lexer lexer = new Lexer();

            string filename = "lex1.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);

            List<Token> expected = new List<Token>()
            {
                // First line
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Bool", line: 1, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "flaga", line: 1, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 1, startPos: 16, filename: filename),
                new Token(keyword: TokenType.Bool, value: "False", line: 1, startPos: 18, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 1, startPos: 23, filename: filename),

                // Second line is comment

                // First line
                new Token(keyword: TokenType.Var, value: "Var", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 3, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "counter", line: 3, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 3, startPos: 17, filename: filename),
                new Token(keyword: TokenType.Int, value: "1024i", line: 3, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 3, startPos: 24, filename: filename),

                // Forth line is \r\n

                // Fifth line
                new Token(keyword: TokenType.Var, value: "Var", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Float", line: 5, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "pi", line: 5, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 5, startPos: 14, filename: filename),
                new Token(keyword: TokenType.Float, value: "3.14f", line: 5, startPos: 16, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 5, startPos: 21, filename: filename),
                
                // Sixth line is comment

                // Seventh line
                new Token(keyword: TokenType.Var, value: "Var", line: 7, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "String", line: 7, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "name", line: 7, startPos: 12, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 7, startPos: 17, filename: filename),
                new Token(keyword: TokenType.String, value: "\"Jaworek\"", line: 7, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 7, startPos: 28, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 7, startPos: 29, filename: filename),
            };

            List<Token> actual = lexer.Tokenize().ToList();

            //Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                bool isSame = actualToken.Equals(expectedToken);

                if(isSame == false)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach(Token tok in actual)
                    {
                        sb.AppendLine($"{tok.Keyword}, '{tok.LiteralValue}', '{tok.Start}'");
                    }

                    string str = sb.ToString();
                    Console.WriteLine(str);

                    Assert.Fail(str);
                    //Assert.Fail($"Tokens at position '{i}' are different!");
                }
            }
        }


        [Fact]
        public void Lex2()
        {
            Lexer lexer = new Lexer();

            string filename = "lex2.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Function, value: "Function", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Bool", line: 1, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "IsGreater", line: 1, startPos: 15, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 1, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 25, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 1, startPos: 29, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 1, startPos: 33, filename: filename),
                new Token(keyword: TokenType.Comma, value: ",", line: 1, startPos: 34, filename: filename),
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 36, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 1, startPos: 40, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "B", line: 1, startPos: 44, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 1, startPos: 45, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 1, startPos: 46, filename: filename),

                // Second line
                new Token(keyword: TokenType.If, value: "If", line: 2, startPos: 2, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 2, startPos: 4, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 2, startPos: 5, filename: filename),
                new Token(keyword: TokenType.RightArrow, value: ">", line: 2, startPos: 7, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "B", line: 2, startPos: 9, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 2, startPos: 10, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 2, startPos: 11, filename: filename),
                
                // Third line
                new Token(keyword: TokenType.Return, value: "Return", line: 3, startPos: 3, filename: filename),
                new Token(keyword: TokenType.Bool, value: "True", line: 3, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 3, startPos: 14, filename: filename),
                
                //  Forth line
                new Token(keyword: TokenType.RightBracket, value: "}", line: 4, startPos: 2, filename: filename),

                //  Fifth line
                new Token(keyword: TokenType.Else, value: "Else", line: 5, startPos: 2, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 5, startPos: 6, filename: filename),
                
                //  Sixth line
                new Token(keyword: TokenType.Return, value: "Return", line: 6, startPos: 3, filename: filename),
                new Token(keyword: TokenType.Bool, value: "False", line: 6, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 6, startPos: 15, filename: filename),
                
                // Seventh line
                new Token(keyword: TokenType.RightBracket, value: "}", line: 7, startPos: 2, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 7, startPos: 3, filename: filename),

                new Token(keyword: TokenType.RightBracket, value: "}", line: 8, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 8, startPos: 2, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 8, startPos: 3, filename: filename),
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

        [Fact]
        public void Lex3()
        {
            Lexer lexer = new Lexer();

            string filename = "lex3.eden";
            string executionLocation = GetLexerSourceFile(filename);

            lexer.LoadFile(executionLocation);
            List<Token> actual = lexer.Tokenize().ToList();

            Token[] expected =
            [
                // First line
                new Token(keyword: TokenType.Function, value: "Function", line: 1, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Bool", line: 1, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "PowOfTwo", line: 1, startPos: 15, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 1, startPos: 23, filename: filename),
                new Token(keyword: TokenType.Var, value: "Var", line: 1, startPos: 24, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 1, startPos: 28, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 1, startPos: 32, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 1, startPos: 33, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 1, startPos: 34, filename: filename),

                // Second line
                new Token(keyword: TokenType.Return, value: "Return", line: 2, startPos: 2, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 2, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Star, value: "*", line: 2, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 2, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 2, startPos: 12, filename: filename),

                // Third line
                new Token(keyword: TokenType.RightBracket, value: "}", line: 3, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 3, startPos: 2, filename: filename),

                // Forth line is comment

                // Fifth line
                new Token(keyword: TokenType.Function, value: "Function", line: 5, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Bool", line: 5, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "PowOfThree", line: 5, startPos: 15, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 5, startPos: 25, filename: filename),
                new Token(keyword: TokenType.Var, value: "Var", line: 5, startPos: 26, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 5, startPos: 30, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 5, startPos: 34, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 5, startPos: 35, filename: filename),
                new Token(keyword: TokenType.LeftBracket, value: "{", line: 5, startPos: 36, filename: filename),

                // Sixth line
                new Token(keyword: TokenType.Return, value: "Return", line: 6, startPos: 2, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 6, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Star, value: "*", line: 6, startPos: 10, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 6, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Star, value: "*", line: 6, startPos: 12, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "A", line: 6, startPos: 13, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 6, startPos: 14, filename: filename),

                // Seventh line
                new Token(keyword: TokenType.RightBracket, value: "}", line: 7, startPos: 1, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 7, startPos: 2, filename: filename),

                // Eight line is comment

                // Ninth line
                new Token(keyword: TokenType.Var, value: "Var", line: 9, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 9, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "x", line: 9, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 9, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "PowOfTwo", line: 9, startPos: 13, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 9, startPos: 21, filename: filename),
                new Token(keyword: TokenType.Int, value: "2i", line: 9, startPos: 22, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 9, startPos: 24, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 9, startPos: 25, filename: filename),

                // Tenth line
                new Token(keyword: TokenType.Var, value: "Var", line: 10, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 10, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "y", line: 10, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 10, startPos: 11, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "PowOfThree", line: 10, startPos: 13, filename: filename),
                new Token(keyword: TokenType.LeftParenthesis, value: "(", line: 10, startPos: 23, filename: filename),
                new Token(keyword: TokenType.Int, value: "2i", line: 10, startPos: 24, filename: filename),
                new Token(keyword: TokenType.RightParenthesis, value: ")", line: 10, startPos: 26, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 10, startPos: 27, filename: filename),

                // Tenth line
                new Token(keyword: TokenType.Var, value: "Var", line: 11, startPos: 1, filename: filename),
                new Token(keyword: TokenType.VariableType, value: "Int", line: 11, startPos: 5, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "sum", line: 11, startPos: 9, filename: filename),
                new Token(keyword: TokenType.Assign, value: "=", line: 11, startPos: 13, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "x", line: 11, startPos: 15, filename: filename),
                new Token(keyword: TokenType.Plus, value: "+", line: 11, startPos: 17, filename: filename),
                new Token(keyword: TokenType.Identifier, value: "y", line: 11, startPos: 19, filename: filename),
                new Token(keyword: TokenType.Semicolon, value: ";", line: 11, startPos: 20, filename: filename),

                new Token(keyword: TokenType.Eof, value: "\0", line: 11, startPos: 21, filename: filename),
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
