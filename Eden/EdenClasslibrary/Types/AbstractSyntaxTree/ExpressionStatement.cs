using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }
        public ExpressionStatement(Token token) : base(token)
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
    }
}
