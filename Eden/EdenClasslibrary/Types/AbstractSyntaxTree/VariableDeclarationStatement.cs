using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    /// <summary>
    /// Variable declaration expression. Example:
    ///     'Var Int counter = 50;'
    /// </summary>
    public class VariableDeclarationStatement : Statement
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Identifier { get; set; }
        public Expression Expression { get; set; }
        public VariableDeclarationStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(VariableDeclarationStatement)} {{");
            sb.AppendLine($"\t{Type.ToAST()},");
            sb.AppendLine($"\t{Identifier.ToString()},");
            sb.AppendLine($"\t{Expression.ToString()}");
            sb.AppendLine("}");
            string result = sb.ToString();
            return result;
        }

        public override string Print()
        {
            return $"{Type.ParenthesesPrint()} {Identifier.ParenthesesPrint} {Expression.ParenthesesPrint()}";
        }

        public override string ToAST(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indents)}{nameof(VariableDeclarationStatement)} {{");
            sb.AppendLine($"{Type.ToAST(indents + 1)},");
            sb.AppendLine($"{Identifier.ToAST(indents + 1)},");
            sb.AppendLine($"{Expression.ToAST(indents + 1)}");
            sb.Append($"{Common.IndentCreator(indents)}}}");
            string result = sb.ToString();
            return result;
        }

        public override string ToPrettyAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indent)}{"Variable".Pastel(Color.Orange)} {{");
            sb.AppendLine($"{Type.ToPrettyAST(indent + 1)},");
            sb.AppendLine($"{Identifier.ToPrettyAST(indent + 1)},");
            sb.AppendLine($"{Expression.ToPrettyAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}}");
            string result = sb.ToString();
            return result;
        }
    }
}
