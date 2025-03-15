using EdenClasslibrary.Types.AbstractSyntaxTree.Expressions;
using EdenClasslibrary.Utility;
using System.Text;

namespace EdenClasslibrary.Types.AbstractSyntaxTree.Statements
{
    public class LoopStatement : Statement
    {
        public Statement IndexerStatement { get; set; }
        public Expression Condition { get; set; }
        public Expression IndexerOperation { get; set; }
        public Statement Body { get; set; }
        public LoopStatement(Token token) : base(token) { }

        public override string ToString()
        {
            return Print();
        }

        public override string Print(int indents = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Common.IndentCreator(indents)}Loop({IndexerStatement.Print()} {Condition.Print()}; {IndexerOperation.Print()}) {{");
            sb.AppendLine($"{Body.Print(indents + 1)}");
            sb.Append($"{Common.IndentCreator(indents)}}};");
            string result = sb.ToString();
            return result;
        }

        public override string ToAbstractSyntaxTree(int indent = 0)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Common.IndentCreator(indent)}{nameof(LoopStatement)} {{");
            sb.AppendLine($"{IndexerStatement.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{Condition.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{IndexerOperation.ToAbstractSyntaxTree(indent + 1)},");
            sb.AppendLine($"{Body.ToAbstractSyntaxTree(indent + 1)}");
            sb.Append($"{Common.IndentCreator(indent)}}};");

            string result = sb.ToString();
            return result;
        }
    }
}
