using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class ReturnStatement : Statement
    {
        public Expression Expression { get; set; }
        public ReturnStatement(Token token) : base(token)
        {
        }
        public override string ToString()
        {
            return Print();
        }

        public string ToAbstractSyntaxTree()
        {
            return ToAbstractSyntaxTree();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Common.IndentCreator(indents)}Return");
            sb.Append($" {Expression.Print()}");
            sb.Append(";");
            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}Return {{");
            sb.AppendLine($"{Expression.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
