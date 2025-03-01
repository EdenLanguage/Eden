using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
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
    public class VariableDeclarationStatement : Statement, IPrintable
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Identifier { get; set; }
        public Expression Expression { get; set; }
        public VariableDeclarationStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public override string Print()
        {
            return $"{Type.ParenthesesPrint()} {Identifier.ParenthesesPrint} {Expression.ParenthesesPrint()}";
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"{Common.IndentCreator(indent)}{"Variable".Pastel(Color.Orange)} {{");
            //sb.AppendLine($"{Type.ToPrettyAST(indent + 1)},");
            //sb.AppendLine($"{Identifier.ToPrettyAST(indent + 1)},");
            //sb.AppendLine($"{Expression.ToPrettyAST(indent + 1)}");
            //sb.Append($"{Common.IndentCreator(indent)}}}");
            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Common.IndentCreator(indents)}Var");
            sb.Append($" {Type.PrettyPrint()}");
            sb.Append($" {Identifier.PrettyPrint()}");
            sb.Append($" = ");
            sb.Append($"{(Expression as IPrintable).PrettyPrint()};");
            string result = sb.ToString();
            return result;
        }
    }
}
