using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class CharExpression : VariableValueExpression, IPrintable
    {
        public CharExpression(Token token) : base(token, typeof(CharObject)) { }

        public char Value
        {
            get
            {
                char parsed = '\0';
                bool couldParse = char.TryParse(NodeToken.LiteralValue, out parsed);
                return parsed;
            }
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public string PrettyPrint(int indents = 0)
        {
            string symbol = Value > 31 ? $"{Value}" : "";
            return $"{Common.IndentCreator(indents)}'{symbol}' or {(int)Value}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            string symbol = Value > 31 ? $"{Value}" : "";
            sb.Append($"{Common.IndentCreator(indent)}{nameof(CharExpression)} {{ '{symbol}' or {(int)Value} }}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}
