using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class IndexExpression : Expression, IPrintable
    {
        public Expression Object { get; set; }
        public Expression Index { get; set; }
        public IndexExpression(Token token) : base(token) { }

        public string PrettyPrint(int indents = 0)
        {
            return $"{Common.IndentCreator(indents)}{(Object as IPrintable).PrettyPrint()}[{(Index as IPrintable).PrettyPrint()}]";
        }

        public override string ToString()
        {
            return PrettyPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(IndexExpression)} {{");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Object {{");
            sb.AppendLine($"{(Object as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}},");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}Index {{");
            sb.AppendLine($"{(Index as IPrintable).PrettyPrintAST(indent + 2)}");
            sb.AppendLine($"{Common.IndentCreator(indent + 1)}}}");
            sb.Append($"{Common.IndentCreator(indent)}}}");

            string toStr = sb.ToString();
            return toStr;
        }
    }
}
