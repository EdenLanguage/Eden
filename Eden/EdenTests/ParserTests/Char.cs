using EdenClasslibrary.Parser;
using EdenClasslibrary.Types.AbstractSyntaxTree;

namespace EdenTests.ParserTests
{
    public class Char
    {
        [Fact]
        public void AssingVariable()
        {
            string[] code =
            [
                "Var Char symbol = '\x1b';",
                "Var Char symbol = 100c;",
                "Var Char symbol = '1';",
                "Var Char symbol = '\0';",
                "Var Char symbol = '\n';",
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Parser parser = new Parser();
                FileStatement block = parser.Parse(file);

                string AST = block.ToASTFormat();
                string STR = block.ToString();

                Assert.True(parser.Errors.Length == 0);
            }
        }

        [Fact]
        public void CharIntegral()
        {
            string[] code =
            [
                "'0';",
                "'\n';",
                "'\u001b';",
                "100c;",
                "0c;",
                "255c;",
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Parser parser = new Parser();
                FileStatement block = parser.Parse(file);

                string AST = block.ToASTFormat();
                string STR = block.ToString();

                //Assert.True(parser.Errors.Length == 0);
            }
        }

        [Fact]
        public void CharOperations()
        {
            //  String + Char = String
            //  Char + Char = Char

            string[] code =
            [
                "'0' + 100c;",          //  Char(148) becasue '0' == 48
                "'1' + '2';",           //  Char(99) because '1' == 49 and '2' == 50
                "200c + 100c;",         //  Char(45) because of the overflow > 255
                "'\n' + '\n';",         //  Char(20)
                "\"string\" + '\n';",   //  String("string\n")  
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Parser parser = new Parser();
                FileStatement block = parser.Parse(file);

                string AST = block.ToASTFormat();
                string STR = block.ToString();
            }
        }
    }
}