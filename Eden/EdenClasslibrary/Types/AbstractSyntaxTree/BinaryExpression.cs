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
    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public BinaryExpression(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return $"{Left.ToString()}{NodeToken.LiteralValue}{Right.ToString()}";
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tLeft: {Left.ToString()}");
            //sb.AppendLine($"\tSymbol: {NodeToken.LiteralValue}");
            //sb.AppendLine($"\tRight: {Right.ToString()}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }

        public override string ToAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BinaryExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent+1)}Left {{");
            sb.AppendLine($"{Left.ToAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent+1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent+1)}Right {{");
            sb.AppendLine($"{Right.ToAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent+1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");
            string toStr = sb.ToString();
            return toStr;
        }

        public override string ParenthesesPrint()
        {
            return $"({Left.ParenthesesPrint()}{NodeToken.LiteralValue}{Right.ParenthesesPrint()})";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(BinaryExpression).Pastel(Color.Orange)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Left {{");
            sb.AppendLine($"{Left.ToPrettyAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Operator: {NodeToken.LiteralValue}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Right {{");
            sb.AppendLine($"{Right.ToPrettyAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");
            string toStr = sb.ToString();
            return toStr;
        }
    }
}
