using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Expressions
{
    public class VariableDefinitionExpression : Expression
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Name { get; set; }
        public VariableDefinitionExpression(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}Var {Type.Print()} {Name.Print()}";
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(VariableDefinitionExpression)} {{");
            sb.AppendLine($"{Type.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{Name.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string result = sb.ToString();
            return result;
        }
    }
}
