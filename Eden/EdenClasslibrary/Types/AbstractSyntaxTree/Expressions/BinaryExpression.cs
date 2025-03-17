using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    /// <summary>
    /// Binary expression is an expression that consists of two expressions and symbol token. Example: 5 + 5
    /// </summary>
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public BinaryExpression(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"({Left.Print()}{NodeToken.LiteralValue}{Right.Print()})";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BinaryExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Left {{");
            sb.AppendLine($"{Left.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Operator: {NodeToken.LiteralValue}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Right {{");
            sb.AppendLine($"{Right.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}