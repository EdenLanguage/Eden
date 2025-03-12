using EdenClasslibrary.Types;

namespace EdenTests.LexerTests
{
    public class Char
    {
        [Fact]
        public void Type()
        {
            string[] code =
            [
                "Char",
                "Char;",
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Lexer lexer = new Lexer();
                lexer.SetInput(file);

                List<Token> actual = lexer.Tokenize().ToList();
            }
        }

        [Fact]
        public void RawLiteral()
        {
            string[] code =
            [
                "'a';",
                "'b';",
                "'c';",
                "' ';",
                "'1';",
                "'9';",
                "'\0';",
                "'\t';",
                "'\r';",
                "'\n';",
            ];

            string[] expected =
            [
                "a",
                "b",
                "c",
                " ",
                "1",
                "9",
                "\0",
                "\t",
                "\r",
                "\n",
            ];

            Assert.Equal(code.Length, expected.Length);

            string file = string.Empty;
            string output = string.Empty;

            for(int i = 0; i < code.Length; i++)
            {
                file = code[i];
                output = expected[i];

                Lexer lexer = new Lexer();
                lexer.SetInput(file);

                List<Token> actual = lexer.Tokenize().ToList();

                Assert.Equal(3, actual.Count);

                Assert.Equal(actual[0].LiteralValue, output);
            }
        }

        [Fact]
        public void MultipleRawLiterals()
        {
            string[] code =
            [
                "'a''b''c''d''e''f'",
            ];

            string[] expected =
            [
                "abcdef",
            ];

            Assert.Equal(code.Length, expected.Length);

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];
                output = expected[i];

                Lexer lexer = new Lexer();
                lexer.SetInput(file);

                List<Token> actual = lexer.Tokenize().ToList();

                //  +1 because of EOF!!
                Assert.Equal(output.Length + 1, actual.Count);

                for(int j = 0; j < output.Length; j++)
                {
                    Assert.Equal(output[j].ToString(), actual[j].LiteralValue);
                }
            }
        }

        [Fact]
        public void CharIntegral()
        {
            string[] code =
            [
                "100c",
                "0c",
                "255c",
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Lexer lexer = new Lexer();
                lexer.SetInput(file);

                List<Token> actual = lexer.Tokenize().ToList();

                //  +1 because of EOF!!
                Assert.Equal(2, actual.Count);
            }
        }
    }
}