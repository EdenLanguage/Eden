using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class ExpressionStatement : Statement
    {
        public Expression Expression { get; set; }
        public ExpressionStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Expression.Print()};";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ExpressionStatement)} {{");
            sb.AppendLine($"{Expression.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
