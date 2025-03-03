using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IntExpression : Expression, IPrintable
    {
        public int Value
        {
            get
            {
                int parsed = 0;
                bool couldParse = int.TryParse(NodeToken.LiteralValue, out parsed);
                return parsed;
            }
        }
        public IntExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public override string ParenthesesPrint()
        {
            return $"{Value}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Value}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indent)}{nameof(IntExpression)} {{ {Value} }}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}
