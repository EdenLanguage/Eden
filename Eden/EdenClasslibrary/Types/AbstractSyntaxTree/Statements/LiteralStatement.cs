using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class LiteralStatement : Statement
    {
        public LiteralStatement(Token token) : base(token) { }
        public Expression Expression { get; set; }
        public Expression Name { get; set; }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Common.IndentCreator(indents)}Literal ");
            sb.Append($"{Expression.Print()}");
            sb.Append($" As ");
            sb.Append($"{Name.Print()};");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(LiteralStatement)} {{");
            sb.AppendLine($"{Expression.ToAbstractSyntaxTree(indent + 1)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}As ");
            sb.AppendLine($"{Name.ToAbstractSyntaxTree(indent + 1)}");
            sb.AppendLine($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}