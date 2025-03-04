using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class UnaryExpression : Expression, IPrintable
    {
        public Token Prefix
        {
            get
            {
                return NodeToken;
            }
        }
        public Expression Expression { get; set; }
        public UnaryExpression(Token token) : base(token)
        {
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
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(UnaryExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Prefix {{ {Prefix.LiteralValue} }},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Expression {{");
            sb.AppendLine($"{(Expression as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Prefix.LiteralValue}{Expression}";
        }
    }
}
