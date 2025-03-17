using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class CharExpression : VariableValueExpression
    {
        public CharExpression(Token token) : base(token, typeof(CharObject)) { }

        public char Value { get; set; }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            string symbol = Value > 31 ? $"{Value}" : "";
            return $"{Common.IndentCreator(indents)}'{symbol}' or {(int)Value}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            string symbol = Value > 31 ? $"{Value}" : "";
            sb.Append($"{Common.IndentCreator(indent)}{nameof(CharExpression)} {{ '{symbol}' or {(int)Value} }}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}