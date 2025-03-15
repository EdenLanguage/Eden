using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class SisyphusStatement : Statement, IPrintable
    {
        public Statement Body { get; set; }

        public SisyphusStatement(Token token) : base(token)
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indents)}Sisyphus {{");
            sb.AppendLine($"{(Body as IPrintable).PrettyPrint(indents + 1)}");
            sb.Append($"{Common.IndentCreator(indents)}}};");

            string result = sb.ToString();
            return result;
        }

        public string ToASTFormat()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrintAST(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(SisyphusStatement)} {{");
            sb.AppendLine($"{(Body as IPrintable).PrettyPrintAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
