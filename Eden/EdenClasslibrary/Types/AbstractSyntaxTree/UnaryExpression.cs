using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class UnaryExpression : Expression
    {
        public Expression Expression { get; set; }
        public UnaryExpression(Token token) : base(token)
        {
        }

        private string EvaluatePrint()
        {
            if(NodeToken.Keyword == TokenType.Minus)
            {
                return $"({NodeToken.LiteralValue}{Expression.ParenthesesPrint()})";
            }
            else
            {
                return $"{Expression.ParenthesesPrint()}";
            }
        }

        public override string ParenthesesPrint()
        {
            return EvaluatePrint();
        }

        public override string ToString()
        {
            return EvaluatePrint();
        }

        public override string ToAST(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(UnaryExpression)} {{");
            sb.AppendLine($"\t{Expression.ToAST()}");
            sb.AppendLine("}");
            string toStr = sb.ToString();
            return toStr;
        }

        public override string ToPrettyAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(UnaryExpression).Pastel(Color.Orange)} {{");
            sb.AppendLine($"\t{Expression.ToPrettyAST()}");
            sb.AppendLine("}");
            string toStr = sb.ToString();
            return toStr;
        }
    }
}
