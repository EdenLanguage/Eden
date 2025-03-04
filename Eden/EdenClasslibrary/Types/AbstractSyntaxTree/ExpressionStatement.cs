using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ExpressionStatement : Statement, IPrintable
    {
        public Expression Expression { get; set; }
        public ExpressionStatement(Token token) : base(token)
        {
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

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(ExpressionStatement)} {{");
            sb.AppendLine($"{(Expression as IPrintable).PrettyPrintAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            return $"{(Expression as IPrintable).PrettyPrint()};";
        }
    }
}
