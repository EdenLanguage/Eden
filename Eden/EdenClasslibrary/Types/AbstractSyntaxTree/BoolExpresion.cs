using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class BoolExpresion : VariableValueExpression, IPrintable
    {
        public BoolExpresion(Token token) : base(token, typeof(BoolObject)) { }

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
            return $"{Common.IndentCreator(indent)}{nameof(BoolExpresion)} {{ {Value} }}";
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Value}";
        }
    }
}
