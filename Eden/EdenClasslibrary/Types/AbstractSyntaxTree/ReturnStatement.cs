using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; set; }
        public ReturnStatement(Token token) : base(token)
        {
        }
        public override string ToString()
        {
            return Expression.ToString();
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("{");
            //sb.AppendLine($"\tExpression: {Expression.ToString()}");
            //sb.AppendLine("}");
            //string result = sb.ToString();
            //return result;
        }

        public override string Print()
        {
            return Expression.ParenthesesPrint();
        }

        public override string ToAST(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indents)}{nameof(ReturnStatement)} {{");
            sb.AppendLine($"{Expression.ToAST(indents+1)}");
            sb.Append($"{Common.IndentCreator(indents)}}}");
            string result = sb.ToString();
            return result;
        }

        public override string ToPrettyAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{"Return".Pastel(Color.Orange)} {{");
            sb.AppendLine($"{Expression.ToPrettyAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}}");
            string result = sb.ToString();
            return result;
        }
    }
}
