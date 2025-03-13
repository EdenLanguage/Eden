using EdenClasslibrary.Types.AbstractSyntaxTree.Interfaces;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree
{
    public class LoopStatement : Statement, IPrintable
    {
        public Statement IndexerStatement { get; set; }
        public Expression Condition { get; set; }
        public Expression IndexerOperation { get; set; }
        public Statement Body { get; set; }
        public LoopStatement(Token token) : base(token) { }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public string PrettyPrint(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indents)}Loop({(IndexerStatement as IPrintable).PrettyPrint()} {(Condition as IPrintable).PrettyPrint()}; {(IndexerOperation as IPrintable).PrettyPrint()}) {{");
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

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(LoopStatement)} {{");
            sb.AppendLine($"{(IndexerStatement as IPrintable).PrettyPrintAST(indent + 1)},");
            sb.AppendLine($"{(Condition as IPrintable).PrettyPrintAST(indent + 1)},");
            sb.AppendLine($"{(IndexerOperation as IPrintable).PrettyPrintAST(indent + 1)},");
            sb.AppendLine($"{(Body as IPrintable).PrettyPrintAST(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
