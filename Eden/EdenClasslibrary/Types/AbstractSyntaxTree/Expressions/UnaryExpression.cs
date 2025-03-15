using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class UnaryExpression : Expression
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
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Prefix.LiteralValue}{Expression}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(UnaryExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Prefix {{ {Prefix.LiteralValue} }},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Expression {{");
            sb.AppendLine($"{Expression.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}
