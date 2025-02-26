using Pastel;
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

        public override string ParenthesesPrint()
        {
            return $"({Left.ParenthesesPrint()}{NodeToken.LiteralValue}{Right.ParenthesesPrint()})";
        }
    }
}
