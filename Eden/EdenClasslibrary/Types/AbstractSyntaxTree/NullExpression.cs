using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class NullExpression : VariableValueExpression, IPrintable
    {
        public object Value
        {
            get
            {
                return null;
            }
        }
        public NullExpression(Token token) : base(token, typeof(NullObject)) { }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}null";
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indent)}{nameof(NullExpression)} {{ null }}");

            string toStr = sb.ToString();
            return toStr;
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public override string ToString()
        {
            return PrettyPrint();
        }
    }
}
