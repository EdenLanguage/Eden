using EdenClasslibrary.Parser;
using EdenClasslibrary.Types;
using EdenClasslibrary.Types.AbstractSyntaxTree;
using EdenClasslibrary.Types.LanguageTypes;
using Environment = EdenClasslibrary.Types.Environment;

namespace EdenTests.EvaluatorTests
{
    public class Char
    {
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

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                Parser parser = new Parser();
                FileStatement block = parser.Parse(file);
                
                string AST = block.ToASTFormat();
                string STR = block.ToString();

                IObject result = evaluator.Evaluate(block, env);
            }
        }

        [Fact]
        public void VariableDeclarations()
        {
            //  String + Char = String
            //  Char + Char = Char

            string[] code =
            [
                "Var Char symbol = '2';",
                "Var Char symbol = '1';",
                "Var Char symbol = '\n';",
                "Var Char symbol = '\t';",
                "Var Char symbol = '\r';",
                "Var Char symbol = '5';",
                "Var Char symbol = 100c;",
                "Var Char symbol = 0c;",
                "Var Char symbol = 256c;",
                "Var Char symbol = 1c;",
            ];

            string file = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < code.Length; i++)
            {
                file = code[i];

                Evaluator evaluator = new Evaluator();
                Environment env = new Environment();
                Parser parser = new Parser();
                FileStatement block = parser.Parse(file);

                string AST = block.ToASTFormat();
                string STR = block.ToString();

                IObject result = evaluator.Evaluate(block, env);
            }
        }
    }
}
