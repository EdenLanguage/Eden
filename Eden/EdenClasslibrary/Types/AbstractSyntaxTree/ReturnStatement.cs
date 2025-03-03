using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using Pastel;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class ReturnStatement : Statement, IPrintable
    {
        public Expression Expression { get; set; }
        public ReturnStatement(Token token) : base(token)
        {
        }
        public override string ToString()
        {
            return PrettyPrint();
        }

        public override string Print()
        {
            return Expression.ParenthesesPrint();
        }

        public string ToASTFormat()
        {
            return PrettyPrintAST();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();
         
            sb.AppendLine($"{Common.IndentCreator(indent)}Return {{");
            sb.AppendLine($"{(Expression as IPrintable).PrettyPrintAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");
            
            string result = sb.ToString();
            return result;
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Common.IndentCreator(indents)}Return");
            sb.Append($" {(Expression as IPrintable).PrettyPrint()}");
            sb.Append(";");
            string result = sb.ToString();
            return result;
        }
    }
}
