using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Binary expression is an expression that consists of two expressions and symbol token. Example:
    ///     5 + 5
    /// </summary>
    public class BinaryExpression : Expression, IPrintable
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public BinaryExpression(Token token) : base(token)
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

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BinaryExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Left {{");
            sb.AppendLine($"{(Left as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Operator: {NodeToken.LiteralValue}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Right {{");
            sb.AppendLine($"{(Right as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"({(Left as IPrintable).PrettyPrint()}{NodeToken.LiteralValue}{(Right as IPrintable).PrettyPrint()})";
        }
    }
}
