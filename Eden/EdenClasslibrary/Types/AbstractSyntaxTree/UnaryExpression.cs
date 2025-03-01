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
            return PrettyPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"{nameof(UnaryExpression).Pastel(Color.Orange)} {{");
            //sb.AppendLine($"\t{Expression.ToPrettyAST()}");
            //sb.AppendLine("}");
            string toStr = sb.ToString();
            return toStr;
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Prefix.LiteralValue}{Expression}";
        }
    }
}
