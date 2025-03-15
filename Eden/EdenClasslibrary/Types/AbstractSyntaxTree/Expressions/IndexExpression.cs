using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class IndexExpression : Expression
    {
        public Expression Object { get; set; }
        public Expression Index { get; set; }
        public IndexExpression(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }
        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{Object.Print()}[{Index.Print()}]";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(IndexExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Object {{");
            sb.AppendLine($"{Object.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Index {{");
            sb.AppendLine($"{Index.ToAbstractSyntaxTree(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}
