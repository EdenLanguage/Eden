using EdenClasslibrary.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdenTests
{
    public class LexterTests
    {
        [Fact]
        public void LexterTest_BasicTokens()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "+{}()=+-;";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Minus, "-"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_BasicTokensWhiteSpace()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "+{}  ()=+  -;";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Plus, "+"),
                new Token(TokenType.Minus, "-"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for(int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_VariableAssingment()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "var int zmienna = 5;";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Var, "var"),
                new Token(TokenType.VarType, "int"),
                new Token(TokenType.Indentifier, "zmienna"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Number, "5"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_FunctionDefinition()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "function int Calculator(var int A){\r\n\treturn A\r\n};";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "function"),
                new Token(TokenType.VarType, "int"),
                new Token(TokenType.Indentifier, "Calculator"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Var, "var"),
                new Token(TokenType.VarType, "int"),
                new Token(TokenType.Indentifier, "A"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.LeftBracket, "{"),
                new Token(TokenType.Keyword, "return"),
                new Token(TokenType.Indentifier, "A"),
                new Token(TokenType.RightBracket, "}"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_FunctionCall()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "var int result = Calculator(5);";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Var, "var"),
                new Token(TokenType.VarType, "int"),
                new Token(TokenType.Indentifier, "result"),
                new Token(TokenType.Assign, "="),
                new Token(TokenType.Indentifier, "Calculator"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Number, "5"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_Equals()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "counter == 5";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Indentifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Number, "5"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_IfStatement()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "if(counter == 5) return 10;";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "if"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Indentifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Number, "5"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Keyword, "return"),
                new Token(TokenType.Number, "10"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_ElseIfStatement()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "else if(counter == true) return 20;";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "else"),
                new Token(TokenType.Keyword, "if"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Indentifier, "counter"),
                new Token(TokenType.Equal, "=="),
                new Token(TokenType.Keyword, "true"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Keyword, "return"),
                new Token(TokenType.Number, "20"),
                new Token(TokenType.Semicolon, ";"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }

        [Fact]
        public void LexterTest_GreaterLesser()
        {
            Lexer lexer = new Lexer();
            lexer.Input = "if(variable <= 0)";

            List<Token> expected = new List<Token>()
            {
                new Token(TokenType.Keyword, "if"),
                new Token(TokenType.LeftParenthesis, "("),
                new Token(TokenType.Indentifier, "variable"),
                new Token(TokenType.LesserOrEqual, "<="),
                new Token(TokenType.Number, "0"),
                new Token(TokenType.RightParenthesis, ")"),
                new Token(TokenType.Eof, "\0"),
            };

            List<Token> actual = lexer.Tokenize();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Token expectedToken = expected[i];
                Token actualToken = actual[i];

                Assert.Equal(expectedToken.Keyword, actualToken.Keyword);
                Assert.Equal(expectedToken.Value, actualToken.Value);
            }
        }
    }
}
