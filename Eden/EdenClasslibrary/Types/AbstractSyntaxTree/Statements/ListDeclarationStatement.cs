using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class ListDeclarationStatement : Statement
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Identifier { get; set; }
        public Expression Expression { get; set; }
        public ListDeclarationStatement(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("List");
            sb.Append($" {Type.Print()}");
            sb.Append($" {Identifier.Print()}");
            sb.Append($" = {Expression.Print()};");

            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ListDeclarationStatement)} {{");
            sb.AppendLine($"{Type.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{Identifier.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{Expression.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}