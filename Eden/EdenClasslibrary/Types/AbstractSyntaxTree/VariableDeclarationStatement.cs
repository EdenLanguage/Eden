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
            sb.AppendLine("{");
            sb.AppendLine($"\tType: {Type.ToString()}");
            sb.AppendLine($"\tIdentifier: {Identifier.ToString()}");
            sb.AppendLine($"\tExpression: {Expression.ToString()}");
            sb.AppendLine("}");
            string result = sb.ToString();
            return result;
        }

        public override string Print()
        {
            return $"{Type.ParenthesesPrint()} {Identifier.ParenthesesPrint} {Expression.ParenthesesPrint()}";
        }
    }
}
