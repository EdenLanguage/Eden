using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class BoolExpresion : Expression, IPrintable
    {
        public bool Value
        {
            get
            {
                bool result = false;
                bool couldParse = bool.TryParse(NodeToken.LiteralValue, out result);
                if (couldParse == false) throw new Exception($"Parser could not parse '{NodeToken.LiteralValue}' to Bool value!");
                return result;
            }
        }
        public BoolExpresion(Token token) : base(token) { }

        public override string ParenthesesPrint()
        {
            return $"{Value}";
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            return $"{nameof(BoolExpresion).Pastel(Color.Orange)}: {Value}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Value}";
        }
    }
}
