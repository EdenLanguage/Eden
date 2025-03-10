using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ListDeclarationStatement : Statement, IPrintable
    {
        public VariableTypeExpression Type { get; set; }
        public IdentifierExpression Identifier { get; set; }
        public Expression Expression { get; set; }
        public ListDeclarationStatement(Token token) : base(token) { }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ListDeclarationStatement)} {{");
            sb.AppendLine($"{(Type as IPrintable).PrettyPrintAST(indent + 1)},");
            sb.AppendLine($"{(Identifier as IPrintable).PrettyPrintAST(indent + 1)},");
            sb.AppendLine($"{(Expression as IPrintable).PrettyPrintAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("List");
            sb.Append($" {(Type as IPrintable).PrettyPrint()}");
            sb.Append($" {(Identifier as IPrintable).PrettyPrint()}");
            sb.Append($" = {(Expression as IPrintable).PrettyPrint()};");

            string result = sb.ToString();
            return result;
        }
    }
}