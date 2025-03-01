using EdenClasslibrary.Utility;
using System;
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

        public override string ToAST(int indents = 0)
        {
            return $"{Common.IndentCreator(indents + 1)}Expression: {Expression.ToAST(indents + 1)}";
        }

        public override string ToPrettyAST(int indent = 0)
        {
            return $"{Common.IndentCreator(indent + 1)}Expression: {Expression.ToPrettyAST(indent + 1)}";
        }
    }
}
